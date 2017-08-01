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
using System;
using System.Runtime.InteropServices;

namespace  vaConnect
{
    public class Credentials
    {
        [DllImport("User32.dll")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);
        public static void cred(String u, String p)
        {
            bool enteredPeapCred = false;
            DateTime timePeapCredStarted = DateTime.Now.AddSeconds(10);

            // wait for PEAP credentials window to appear (max. wait for 10 seconds)
            while (!enteredPeapCred && timePeapCredStarted >= DateTime.Now)
            {
                IntPtr hwndLogon = Win32.FindWindow(null, "User Logon");

                if (hwndLogon != IntPtr.Zero)
                {
                    // move User Logon window offscreen to prevent screen flicker in app
                    Win32.MoveWindow(hwndLogon, -600, 0, 320, 480, true);

                    // "Network Log On" label   
                    IntPtr hwndCtrl1 = Win32.GetWindow(hwndLogon, Win32.GW_CHILD);
                    // "Enter network info..." label
                    IntPtr hwndCtrl2 = Win32.GetWindow(hwndCtrl1, Win32.GW_HWNDNEXT);
                    // "User name:" label
                    IntPtr hwndCtrl3 = Win32.GetWindow(hwndCtrl2, Win32.GW_HWNDNEXT);
                    // username textbox
                    IntPtr hwndCtrl4 = Win32.GetWindow(hwndCtrl3, Win32.GW_HWNDNEXT);
                    // "Password:" label 
                    IntPtr hwndCtrl5 = Win32.GetWindow(hwndCtrl4, Win32.GW_HWNDNEXT);
                    // password textbox 
                    IntPtr hwndCtrl6 = Win32.GetWindow(hwndCtrl5, Win32.GW_HWNDNEXT);
                    // enter password into textbox
                    StringBuilder sbPassword = new StringBuilder();
                    sbPassword.Append(p);
                    Win32.SetWindowText(hwndCtrl6, sbPassword);
                    // "Domain:" label
                    IntPtr hwndCtrl7 = Win32.GetWindow(hwndCtrl6, Win32.GW_HWNDNEXT);
                    // domain textbox
                    IntPtr hwndCtrl8 = Win32.GetWindow(hwndCtrl7, Win32.GW_HWNDNEXT);
                    // "Save password" checkbox 
                    IntPtr hwndCtrl9 = Win32.GetWindow(hwndCtrl8, Win32.GW_HWNDNEXT);
                    // send BST_CHECKED message to set checkbox
                    Win32.SendMessage(hwndCtrl9, Win32.BM_SETCHECK, Win32.BST_CHECKED, 0);

                    // send WM_COMMAND with left softkey to submit user dialog
                    IntPtr hwndMenu = Win32.SHFindMenuBar(hwndLogon);
                    Win32.SendMessage(hwndLogon, Win32.WM_COMMAND, 0x2F87, hwndMenu.ToInt32());

                    enteredPeapCred = true;
                }
            }
        }
    }
}
