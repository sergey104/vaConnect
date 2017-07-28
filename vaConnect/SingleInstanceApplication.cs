

using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.VisualBasic.ApplicationServices;
using System.Collections;

namespace vaConnect
{
    /// <summary>
    /// A SingleInstanceApplication extending the application base. Part of the VB namespaces.
    /// </summary>
    public class SingleInstanceApplication : WindowsFormsApplicationBase
    {
        public SingleInstanceApplication(AuthenticationMode mode) : base(mode)
        {
            InitializeAppProperties();
        }

        public SingleInstanceApplication()
        {
            InitializeAppProperties();
        }

        protected virtual void InitializeAppProperties()
        {
            this.IsSingleInstance = true;
            this.EnableVisualStyles = true;
        }

        public virtual void Run(MainForm mainForm)
        {
            List<string> list = new List<string>(this.CommandLineArgs);
            mainForm.ParseCommandLine(list.ToArray());
            this.MainForm = mainForm;
            this.Run(list.ToArray());
        }
    }
}
