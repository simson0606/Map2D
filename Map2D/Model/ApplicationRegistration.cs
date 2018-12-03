using System;
using System.Linq;
using Microsoft.Win32;

namespace Map2D.Model
{
    class ApplicationRegistration
    {
        public void RegisterApplication(string extension, string applicationName, string applicationPath,
            string iconPath, string description = "")
        {
            WriteRegistrationToRegistry(extension, applicationName, applicationPath, iconPath, description);
        }


        public void UnregisterApplication(string extension, string applicationName)
        {
            WriteDefaultsToRegistry(extension, applicationName);
        }


        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private void WriteRegistrationToRegistry(string extension, string applicationName, string applicationPath,
            string iconPath, string description = "")
        {
            if (Registry.GetValue("HKEY_CLASSES_ROOT\\" + applicationName, String.Empty, String.Empty) == null)
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\" + applicationName, "", extension);
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\" + applicationName, "FriendlyTypeName",
                    description);
                Registry.SetValue(
                    "HKEY_CURRENT_USER\\Software\\Classes\\" + applicationName + "\\shell\\open\\command", "",
                    applicationPath + " \"%1\"");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\" + applicationName + "\\DefaultIcon", "",
                    iconPath + ",0");

                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\." + extension, "", applicationName);

                //this call notifies Windows that it needs to redo the file associations and icons
                SHChangeNotify(0x08000000, 0x2000, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void WriteDefaultsToRegistry(string extension, string applicationName)
        {
            string softwareClassesKeyname = "Software\\Classes\\";

            using (RegistryKey softwareClassesKey = Registry.CurrentUser.OpenSubKey(softwareClassesKeyname, true))
            {
                if (softwareClassesKey != null)
                {
                    if (softwareClassesKey.GetSubKeyNames().Contains(applicationName))
                    {
                        softwareClassesKey.DeleteSubKeyTree(applicationName);
                    }
                    if (softwareClassesKey.GetSubKeyNames().Contains("." + extension))
                    {
                        softwareClassesKey.DeleteSubKeyTree("." + extension);
                    }
                    SHChangeNotify(0x08000000, 0x2000, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }
    }
}