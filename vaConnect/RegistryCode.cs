using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace vaConnect {
    public class Register_it { 
        public
         void registry()
           {
             RegistryKey vaConnect1 = Registry.ClassesRoot.CreateSubKey("VaConnect");
             vaConnect1.SetValue("", "URL:vaconnect Protocol");
             vaConnect1.SetValue("URL Protocol", "");

             RegistryKey defaultIcon = vaConnect1.CreateSubKey("DefaultIcon");
             defaultIcon.SetValue("", Path.GetFileName(Application.ExecutablePath));

             RegistryKey shell = vaConnect1.CreateSubKey("shell");
             RegistryKey open = shell.CreateSubKey("open");
             RegistryKey command = open.CreateSubKey("command");
             command.SetValue("", Application.ExecutablePath + " %1");
           }
}
        }