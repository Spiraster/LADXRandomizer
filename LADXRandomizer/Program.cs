using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LADXRandomizer
{
    static class Program
    {
        internal const bool Debug = true;
        internal const string Version = "0.1.0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //bool debug = args.Length > 0 && (args[0] == "-d" || args[0] == "--debug");
            Application.Run(new MainForm(Debug));
            //new MainForm(true).CreateRom((Settings)Properties.Settings.Default.SettingsValue);
        }

        public static int GetFrameworkVersion()
        {
            using (var key = (RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default)).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
                return (int?)key?.GetValue("Release") ?? 0;
        }
    }
}
