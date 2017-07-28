

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;

namespace vaConnect
{
    /// <summary>
    /// The Main class of the application.
    /// </summary>
    static class Program
    {
        // Private members
        private static MainForm mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Creates a new SingleInstanceApplication (from the VB Namespace)
            SingleInstanceApplication app = new SingleInstanceApplication();
            app.StartupNextInstance += new StartupNextInstanceEventHandler(app_StartupNextInstance);

            //Creates the MainForm and loads the application.
            mainForm = new MainForm();
            app.Run(mainForm);
        }

        /// <summary>
        /// Method executed if the application is allready running.
        /// </summary>
        static void app_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            //Tels the loaded main form to parse the command line arguments.
            List<string> list = new List<string>(e.CommandLine);
            mainForm.ParseCommandLine(list.ToArray());
        }
    }
}