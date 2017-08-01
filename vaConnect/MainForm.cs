//Main application form
// 28.07.2017

using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;
using NativeWifi;
using System.Linq;


namespace vaConnect
{
    /// <summary>
    /// The MainForm of the application. Contains most of the application logic.
    /// </summary>
    public partial class MainForm : Form
    {
        

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Method executed when the form loads. Starts the HttpListener.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
          //  Thread httpThread = new Thread(new ThreadStart(StartHttpListener));
          //  httpThread.IsBackground = true;
          //  httpThread.Start();
            

            installProtocolHandler.Enabled = (Registry.ClassesRoot.OpenSubKey("VaConnect") == null);
            removeProtocolHandler.Enabled = !installProtocolHandler.Enabled;
        }

        /// <summary>
        /// Parses the query string into a dictionary.
        /// </summary>
        /// <param name="query">The query string to parse.</param>
        /// <returns>A key/value dictionary.</returns>
        private Dictionary<string, string> ParseQueryString(string query)
        {
            string[] queryParts = query.Substring(query.IndexOf("?") + 1).Split(new char[] { '&' });
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string part in queryParts)
            {
                string[] keyValue = part.Split(new char[] { '=' });
                dictionary.Add(keyValue[0], keyValue[1]);
            }
            return dictionary;
        }

       
        /// <summary>
        /// Git ssid string name
        /// The method parse name from byte form
        /// </summary>
        /// <param name="ssid"></param>
        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }



        /// <summary>
        /// Executed when the user launches a new instance of the application.
        /// The method parse all the command line arguments.
        /// </summary>
        /// <param name="args"></param>
        public async void ParseCommandLine(string[] args)
        {

            foreach (string s in args)
            {
                String token;
                String identifier;
                String Ssid;
                String password;
                bool valid = Uri.IsWellFormedUriString(s, UriKind.Absolute);
                if (valid)
                {
                    Uri uri = new Uri(s);
                    
                        Dictionary<string, string> parameters = ParseQueryString(uri.Query);
                    String S = uri.Host;
                        if (uri.Host.Equals("onboarding"))
                        {
                        token = parameters["token"];
                        identifier = parameters["identifier"];
                        int numVal = Int32.Parse(identifier);
                        WiFiProfile z = new WiFiProfile();
                        try
                        {
                            z = await OnboardingService.getInstance().getWiFiProfileAsync(token, identifier);
                            Ssid = z.getUser_policies().getSsid();
                        }
                        catch
                        {
                            MessageBox.Show("Unable to get WiFi profile data from server");
                            return;

                        }
                        WiFiConfiguration wc = z.getWifiConfiguration();
                       
                        //////////////////////////////////switch
                        switch(numVal)
                        {
                            case 2:
                                {
                                    WlanClient client = new WlanClient();
                                    Wlan.WlanAvailableNetwork[] wlanBssEntries = null;
                                    int k = 1;
                                    WlanClient.WlanInterface[] wlanIfaces = client.Interfaces;
                                    foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                                    {

                                        try
                                        {
                                            wlanBssEntries = wlanIface.GetAvailableNetworkList(0);
                                        }
                                        catch (Exception ex)
                                        {
                                            String er = ex.ToString();
                                            MessageText.Clear();
                                            MessageText.Text = er;
                                            return;
                                        }


                                        requestList.Items.Clear();
                                        foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                                        {
                                            
                                            ListViewItem listItemWiFi = new ListViewItem();

                                            
                                            listItemWiFi.Text = System.Text.ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);

                                            
                                            
                                            listItemWiFi.SubItems.Add(network.dot11DefaultAuthAlgorithm.ToString().Trim((char)0)); 
                                            listItemWiFi.SubItems.Add(network.dot11DefaultCipherAlgorithm.ToString().Trim((char)0)); 
                                            
                                            
                                            requestList.Items.Add(listItemWiFi);

                                        }
                                        requestList.Sorting = SortOrder.Ascending;
                                        for (int i = 0; i < requestList.Items.Count - 1; i++)
                                        {
                                            if (requestList.Items[i].Tag == requestList.Items[i + 1].Tag)
                                            {
                                                requestList.Items[i + 1].Remove();
                                            }
                                        }
                                        var q = wlanBssEntries.Where(X => GetStringForSSID(X.dot11Ssid) == Ssid).FirstOrDefault();
                                      if(q.profileName == null)
                                     {
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " network not found!";
                                          return;
                                      }
                                        MessageText.Clear();
                                        MessageText.Text = Ssid + " netwok found!";
                                        
                                        try
                                        {
                                            String xprofile = wc.getxml();
                                            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xprofile, true);
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " profile was set!";
                                            
                                        }
                                        catch
                                        {
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " cannot set profile!";
                                            return;
                                        }
                                        try
                                        {
                                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any,q.profileName);
                                            
                                            MessageText.AppendText( Ssid + " profile was connected!");
                                        }
                                        catch
                                        {
                                           
                                            MessageText.AppendText(Ssid + " cannot connect!");
                                            return;
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    WlanClient client = new WlanClient();
                                    Wlan.WlanAvailableNetwork[] wlanBssEntries = null;
                                    int k = 1;
                                    WlanClient.WlanInterface[] wlanIfaces = client.Interfaces;
                                    foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                                    {

                                        try
                                        {
                                            wlanBssEntries = wlanIface.GetAvailableNetworkList(0);
                                        }
                                        catch (Exception ex)
                                        {
                                            String er = ex.ToString();
                                            MessageText.Clear();
                                            MessageText.Text = er;
                                            return;
                                        }


                                        requestList.Items.Clear();
                                        foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                                        {

                                            ListViewItem listItemWiFi = new ListViewItem();


                                            listItemWiFi.Text = System.Text.ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);



                                            listItemWiFi.SubItems.Add(network.dot11DefaultAuthAlgorithm.ToString().Trim((char)0));
                                            listItemWiFi.SubItems.Add(network.dot11DefaultCipherAlgorithm.ToString().Trim((char)0));


                                            requestList.Items.Add(listItemWiFi);

                                        }
                                        requestList.Sorting = SortOrder.Ascending;
                                        for (int i = 0; i < requestList.Items.Count - 1; i++)
                                        {
                                            if (requestList.Items[i].Tag == requestList.Items[i + 1].Tag)
                                            {
                                                requestList.Items[i + 1].Remove();
                                            }
                                        }
                                        var q = wlanBssEntries.Where(X => GetStringForSSID(X.dot11Ssid) == Ssid).FirstOrDefault();
                                        if (q.profileName == null)
                                        {
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " network not found!";
                                            return;
                                        }
                                        MessageText.Clear();
                                        MessageText.Text = Ssid + " netwok found!";

                                        try
                                        {
                                            String xprofile = wc.getxml();
                                            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xprofile, true);
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " profile was set!";

                                        }
                                        catch
                                        {
                                            MessageText.Clear();
                                            MessageText.Text = Ssid + " cannot set profile!";
                                            return;
                                        }
                                        try
                                        {
                                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, q.profileName);

                                            MessageText.AppendText(Ssid + " profile was connected!");
                                        }
                                        catch
                                        {

                                            MessageText.AppendText(Ssid + " cannot connect!");
                                            return;
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    int i = 0;
                                    break;
                                }

                        }

                    } 
                } 
            }
        }

        /// <summary>
        /// Method executed when the user double clicks on a list item.
        /// Opens up the VaConnect Request Form with the selected item.
        /// </summary>
        private void requestList_DoubleClick(object sender, EventArgs e)
        {
            if (requestList.SelectedItems.Count == 1)
            {
                requestList.Items.Clear();
            }
        }

        
/*
        /// <summary>
        /// Hack to make sure the request form is above the main form when the user clicks a link
        /// to open up the application.
        /// </summary>
        void requestForm_Shown(object sender, EventArgs e)
        {
            ((Form)sender).BringToFront();
        }

        */

        /// <summary>
        /// Method executed when the user clicks the button to open up the 
        /// VaConnect Web Application.
        /// </summary>
        private void showWebButton_Click(object sender, EventArgs e)
        {
            WlanClient client = new WlanClient();
            Wlan.WlanAvailableNetwork[] wlanBssEntries = null;
            int k = 1;
            WlanClient.WlanInterface[] wlanIfaces = client.Interfaces;
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                try
                {
                    wlanBssEntries = wlanIface.GetAvailableNetworkList(0);
                }
                catch(Exception ex) {
                    String er = ex.ToString();
                    MessageText.Clear();
                    MessageText.Text = er;
                    return;
                }
                



                requestList.Items.Clear();
                foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                {

                    ListViewItem listItemWiFi = new ListViewItem();


                    listItemWiFi.Text = System.Text.ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);



                    listItemWiFi.SubItems.Add(network.dot11DefaultAuthAlgorithm.ToString().Trim((char)0));
                    listItemWiFi.SubItems.Add(network.dot11DefaultCipherAlgorithm.ToString().Trim((char)0));


                    requestList.Items.Add(listItemWiFi);

                }
                requestList.Sorting = SortOrder.Ascending;
                for (int i = 0; i < requestList.Items.Count - 1; i++)
                {
                    if (requestList.Items[i].Tag == requestList.Items[i + 1].Tag)
                    {
                        requestList.Items[i + 1].Remove();
                    }
                }
                
            }
        }

       
        private void registry()
        {
            if (Registry.ClassesRoot.OpenSubKey("vaConnect") == null)
            {
                try
                {
                    RegistryKey vac = Registry.ClassesRoot.CreateSubKey("VaConnect");
                    vac.SetValue("", "URL:VaConnect Protocol");
                    vac.SetValue("URL Protocol", "");

                    RegistryKey defaultIcon = vac.CreateSubKey("DefaultIcon");
                    defaultIcon.SetValue("", Path.GetFileName(Application.ExecutablePath));

                    RegistryKey shell = vac.CreateSubKey("shell");
                    RegistryKey open = shell.CreateSubKey("open");
                    RegistryKey command = open.CreateSubKey("command");
                    command.SetValue("", Application.ExecutablePath + " %1");
                    installProtocolHandler.Enabled = false;
                    removeProtocolHandler.Enabled = true;
                    MessageBox.Show("Protocol handler registered!", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to register protocol handler. Are you running this application as an administrator?", "Running as administrator?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Protocol handler allready registered!", "Allready registered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

   
        /// <summary>
        /// Method executed when the user clicks the button to register the protocol handler.
        /// The method writes all registry keys needed to register the vaconnect:// protochol handler.
        /// </summary>
        private void installProtocolHandler_Click(object sender, EventArgs e)
        {
            if (Registry.ClassesRoot.OpenSubKey("vaConnect") == null)
            {
                try
                {
                    RegistryKey vac = Registry.ClassesRoot.CreateSubKey("VaConnect");
                    vac.SetValue("", "URL:VaConnect Protocol");
                    vac.SetValue("URL Protocol", "");

                    RegistryKey defaultIcon = vac.CreateSubKey("DefaultIcon");
                    defaultIcon.SetValue("", Path.GetFileName(Application.ExecutablePath));

                    RegistryKey shell = vac.CreateSubKey("shell");
                    RegistryKey open = shell.CreateSubKey("open");
                    RegistryKey command = open.CreateSubKey("command");
                    command.SetValue("", Application.ExecutablePath + " %1");
                    installProtocolHandler.Enabled = false;
                    removeProtocolHandler.Enabled = true;
                    MessageBox.Show("Protocol handler registered!", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to register protocol handler. Are you running this application as an administrator?", "Running as administrator?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Protocol handler allready registered!", "Allready registered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Method executed when the user clicks the button to remove the protocol handler. 
        /// The method deletes all registry keys written by the application.
        /// </summary>
        private void removeProtocolHandler_Click(object sender, EventArgs e)
        {
            if (Registry.ClassesRoot.OpenSubKey("VaConnect") != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree("VaConnect");
                removeProtocolHandler.Enabled = false;
                installProtocolHandler.Enabled = true;
                MessageBox.Show("Protocol handler removed!", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Protocol handler not registered!", "Not registered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            HelpForm hf = new HelpForm();
            hf.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
                String uristr = Properties.Resources.UriVAConnect;
                System.Diagnostics.Process.Start(uristr);
          
        }

    }
    
}