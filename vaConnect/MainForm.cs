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
//using NativeWifi;
using WlanNative;
using System.Linq;


namespace vaConnect
{
    /// <summary>
    /// The MainForm of the application. Contains most of the application logic.
    /// </summary>
    public partial class MainForm : Form
    {

        WlanNative.WlanNative native = null;
        List<WlanInterface> interfaces = null;
        List<WlanNetwork> networks = null;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
        }
        void RefreshInterfaces()
        {
            try
            {
                if (native == null)
                {
                    native = new WlanNative.WlanNative();
                }
                interfaces = native.GetInterfaces();
            }
            catch (Exception ex)
            {
                UIError(ex);
                return;
            }
        }
        void RefreshNetworks()
        {

            foreach (WlanInterface i in interfaces)
            {
                try
                {
                    if (i != null)
                    {
                        i.ScanForNetworks();
                        networks = i.GetAvailableNetworks(false, false);
                        
                    }
                }
                catch (Exception ex)
                {
                    UIError(ex);
                    return;
                }
            }
            
           
        }
        void UIError (Exception e)
        {
            String er = e.ToString();
            MessageBox.Show("Error: "+e, "vsConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            return;
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
    //    static string GetStringForSSID(Wlan.Dot11Ssid ssid)
    //    {
     //       return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
    //    }



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
                String username;
                String profile_name;

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
                            password = z.getUser_policies().getPassword();
                            username = z.getUser_policies().getUsername();
                            profile_name = z.getUser_policies().getProfile_name();
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Unable to get WiFi profile data from server\n" + e.ToString(), "vaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            return;

                        }
                        WiFiConfiguration wc = z.getWifiConfiguration();
                       
                        //////////////////////////////////switch
                       switch(numVal)
                        {
                            case 2:
                                {
                                    RefreshInterfaces();
                                    RefreshNetworks();
                                    requestList.Items.Clear();
                                    foreach (WlanNetwork network in networks)
                                        {
                                            
                                            ListViewItem listItemWiFi = new ListViewItem();
                                            listItemWiFi.Text = network.SSID;
                                            listItemWiFi.SubItems.Add(network.DefaultAuthAlgorithm.ToString());
                                            listItemWiFi.SubItems.Add(network.DefaultCipherAlgorithm.ToString());

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
                                    var q = networks.Where(X => X.SSID == Ssid).FirstOrDefault();
                                    if(q == null)
                                    {
                                        MessageBox.Show(Ssid + " network not found!", "vaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                       
                                      return;
                                     }

                                    MessageBox.Show(Ssid + " network found!", "vaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


                                    
                                    String xprofile = wc.getxml();
                                    foreach (WlanInterface i in interfaces)
                                    {
                                        try
                                        {
                                            WlanProfile p = new WlanProfile();
                                            
                                            i.SetProfile(p, xprofile);
                                            MessageBox.Show(Ssid + " Profile was set!", "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                                                                       
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(Ssid + " Cannot set profile!\n"+e.ToString(), "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                                            return;
                                        }
                                    }
                                    //////////////////////////// Credentials
                                    foreach (WlanInterface i in interfaces)
                                    {
                                        try
                                        {
                                            List<WlanProfile> dd2 = i.GetProfiles();
                                           
                                            var x = dd2.Where(X => X.ProfileName == profile_name).FirstOrDefault();
                                            string template = Properties.Resources.SDK;
                                            string xm = String.Format(template, username, password);
                                            i.SetProfileEAPXmlUserData(x.ProfileName, xm, true);
                                            MessageBox.Show(Ssid + " Profile user and password were set!", "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(Ssid + " Cannot set credentials!\n" + e.ToString(), "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                                            return;
                                        }
                                    }
                                    //////////////////////// Connect
                                    foreach (WlanInterface i in interfaces)
                                    {
                                        try
                                        {
                                            List<WlanProfile> dd2 = i.GetProfiles();

                                            var x = dd2.Where(X => X.ProfileName == profile_name).FirstOrDefault();
                                            i.Connect(x);
                                            MessageBox.Show(Ssid + " Connected!", "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(Ssid + " Cannot connect!\n" + e.ToString(), "vsaConnect", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                                            return;
                                        }
                                    }

                                    /*    try
                                        {
                                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any,q.profileName);

                                            MessageText.AppendText( Ssid + " profile was connected!");
                                        }
                                        catch
                                        {

                                            MessageText.AppendText(Ssid + " cannot connect!");
                                            return;
                                        } */

                                    break;
                                }
                            
                            default:
                                {
                                    
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
            RefreshInterfaces();
            RefreshNetworks();
            requestList.Items.Clear();
            foreach (WlanNetwork network in networks)
            {

                ListViewItem listItemWiFi = new ListViewItem();


                listItemWiFi.Text = network.SSID;



                listItemWiFi.SubItems.Add(network.DefaultAuthAlgorithm.ToString());
                listItemWiFi.SubItems.Add(network.DefaultCipherAlgorithm.ToString());

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