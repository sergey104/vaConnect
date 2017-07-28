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

namespace vaConnect
{
    /// <summary>
    /// The MainForm of the application. Contains most of the application logic.
    /// </summary>
    public partial class MainForm : Form
    {
        private BusinessLogic logic;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            logic = new BusinessLogic();
        }

        /// <summary>
        /// Method executed when the form loads. Starts the HttpListener.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
          //  Thread httpThread = new Thread(new ThreadStart(StartHttpListener));
          //  httpThread.IsBackground = true;
          //  httpThread.Start();
            UpdateRequestList();

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
        /// Updates the list of VaConnect Requests.
        /// </summary>
        private void UpdateRequestList()
        {
            requestList.BeginUpdate();
            requestList.Items.Clear();
        /*    foreach (VaConnectRequest request in logic.RequestDatabase)
            {
                ListViewItem item = new ListViewItem(new string[] {
                        request.ID.ToString(),
                        request.Subject,
                        request.Date.ToString(),
                        request.Closed.ToString() });
                item.Tag = request;
                item.ForeColor = (request.Closed) ? Color.Green : Color.Red;
                requestList.Items.Add(item);
            } */
            requestList.EndUpdate();
        }

        /// <summary>
        /// Starts the Http Listener on a background thread.
        /// </summary>
        private void StartHttpListener()
        {
            if (HttpListener.IsSupported)
            {
                try
                {
                    //Creates a HttpListener with two urls for RSS and HTML.
                    HttpListener listener = new HttpListener();
                    listener.Prefixes.Add("http://localhost:8080/taskfeed/");
                    listener.Prefixes.Add("http://localhost:8080/taskhtml/");
                    listener.Start();
                    while (true)
                    {
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;

                        //Checks if the user is requesting the HTML page or the RSS feed.
                        if (context.Request.RawUrl.EndsWith("feed"))
                        {
                            //Renders the RSS feed.
                            context.Response.ContentType = "application/xml";

                            XmlTextWriter xmlWriter = new XmlTextWriter(context.Response.OutputStream, Encoding.UTF8);
                            xmlWriter.WriteStartDocument();
                            xmlWriter.WriteStartElement("rss");
                            xmlWriter.WriteAttributeString("version", "2.0");
                            xmlWriter.WriteStartElement("channel");
                            xmlWriter.WriteElementString("title", "VaConnect Application - Open Requests");
                            xmlWriter.WriteElementString("description", "An RSS feed of open VaConnect Requests. Can be used by gadgets, feed readers etc.");
                            xmlWriter.WriteElementString("link", "http://localhost:8080/taskhtml/");
                            foreach (VaConnectRequest hr in logic.RequestDatabase)
                            {
                                if (!hr.Closed)
                                {
                                    xmlWriter.WriteStartElement("item");
                                    xmlWriter.WriteElementString("guid", hr.ID.ToString());
                                    xmlWriter.WriteElementString("title", hr.Subject);
                                    xmlWriter.WriteElementString("link", "vaconnect://open?id=" + hr.ID.ToString());
                                    xmlWriter.WriteElementString("pubDate", hr.Date.ToString("r"));
                                    xmlWriter.WriteEndElement();
                                }
                            }
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndDocument();
                            xmlWriter.Flush();
                            xmlWriter.Close();
                        }
                        else
                        {
                            //Renders the HTML page.
                            context.Response.ContentType = "text/html";
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<html><head><title>VaConnect Application</title>");
                            sb.Append("<link rel='alternate' type='application/rss+xml' title='VaConnect Request Feed' href='taskfeed' />");
                            sb.Append("<style>body { font-family: verdana; }</style></head><body>");

                            sb.Append("<h1 style='color: red;'>Open VaConnect Requests</h1><ul>");
                            foreach (VaConnectRequest openItem in logic.RequestDatabase.FindAll(delegate (VaConnectRequest hr) { return !hr.Closed; }))
                            {
                                sb.Append(string.Format("<li><a href=vaconnect://open?id={0}'>{1}</a></li>", openItem.ID, openItem.Subject));
                            }

                            sb.Append("</ul><h1 style='color: green;'>Closed VaConnect Requests</h1><ul>");
                            foreach (VaConnectRequest closedItem in logic.RequestDatabase.FindAll(delegate (VaConnectRequest hr) { return hr.Closed; }))
                            {
                                sb.Append(string.Format("<li><a href='vaconnect://open?id={0}'>{1}</a></li>", closedItem.ID, closedItem.Subject));
                            }
                            sb.Append("</ul></body></html>");

                            StreamWriter streamWriter = new StreamWriter(response.OutputStream);
                            streamWriter.WriteLine(sb.ToString());
                            streamWriter.Flush();
                            streamWriter.Dispose();
                        }
                        response.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to start HttpHandler. Are you running this application as an administrator?", "Running as administrator?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Executed when the user launches a new instance of the application.
        /// The method parse all the command line arguments.
        /// </summary>
        /// <param name="args"></param>
        public void ParseCommandLine(string[] args)
        {
            foreach (string s in args)
            {
                bool valid = Uri.IsWellFormedUriString(s, UriKind.Absolute);
                if (valid)
                {
                    Uri uri = new Uri(s);
                    Dictionary<string, string> parameters = ParseQueryString(uri.Query);

                    if (uri.Host.Equals("open"))
                    {
                        int requestID = Convert.ToInt32(parameters["id"]);
                        VaConnectRequest request = logic.RequestDatabase.Find(delegate (VaConnectRequest r) { return (r.ID == requestID); });
                        if (request != null)
                        {
                            RequestForm requestForm = new RequestForm(request);
                            requestForm.FormClosed += new FormClosedEventHandler(requestForm_FormClosed);
                            requestForm.Shown += new EventHandler(requestForm_Shown);
                            requestForm.Show();
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
                RequestForm requestForm = new RequestForm((VaConnectRequest)requestList.SelectedItems[0].Tag);
                requestForm.FormClosed += new FormClosedEventHandler(requestForm_FormClosed);
                requestForm.Show();
            }
        }

        /// <summary>
        /// Method executed when the user clicks the button to create a new VaConnect Request.
        /// </summary>
        private void newRequest_Click(object sender, EventArgs e)
        {
            VaConnectRequest newItem = new VaConnectRequest();
            newItem.Subject = "New Request...";
            newItem.Closed = false;
            newItem.Date = DateTime.Now;
            newItem.ID = logic.RequestDatabase.Count + 1;
            logic.RequestDatabase.Add(newItem);

            RequestForm requestForm = new RequestForm(newItem);
            requestForm.FormClosed += new FormClosedEventHandler(requestForm_FormClosed);
            requestForm.Show();
        }

        /// <summary>
        /// Hack to make sure the request form is above the main form when the user clicks a link
        /// to open up the application.
        /// </summary>
        void requestForm_Shown(object sender, EventArgs e)
        {
            ((Form)sender).BringToFront();
        }

        /// <summary>
        /// Method executed when the user closes a  request form. 
        /// Updates the list of active requests.
        /// </summary>
        void requestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateRequestList();
        }

        /// <summary>
        /// Method executed when the user clicks the button to open up the 
        /// VaConnect Web Application.
        /// </summary>
        private void showWebButton_Click(object sender, EventArgs e)
        {
            Process.Start("http://localhost:8080/taskhtml/");
        }

        /// <summary>
        /// Method executed when the user clicks the "about" button.
        /// </summary>
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Example application written by Jonas Follesø\r\nhttp://jonas.follesoe.no", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}