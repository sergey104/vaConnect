// Copyright (c) 2007, Jonas Follesø
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the Jonas Follesø nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY Jonas Follesø ``AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL Jonas Follesø BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;

namespace HelpDeskClient
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