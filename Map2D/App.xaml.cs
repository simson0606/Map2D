using System;
using System.Linq;
using System.Windows;
using Map2D.Model;

namespace Map2D
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                var args = Environment.GetCommandLineArgs();
                string extension = "m2d";
                string applicationName = "Map2D";
                string icon = "icon.ico";
                string description = "Map2D map file";
                if (args.Any(x => x.Equals("/register", StringComparison.OrdinalIgnoreCase)))
                {
                    ApplicationRegistration registration = new ApplicationRegistration();
                    registration.RegisterApplication(extension, applicationName,
                        AppDomain.CurrentDomain.BaseDirectory + applicationName + ".exe",
                        AppDomain.CurrentDomain.BaseDirectory + icon, description);
                    Environment.Exit(0);
                }
                if (args.Any(x => x.Equals("/unregister", StringComparison.OrdinalIgnoreCase)))
                {
                    ApplicationRegistration registration = new ApplicationRegistration();
                    registration.UnregisterApplication(extension, applicationName);
                    Environment.Exit(0);
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.ReadLine();
            }
        }
    }
}
