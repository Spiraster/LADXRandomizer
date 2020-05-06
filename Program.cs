using System;
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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static int GetFrameworkVersion()
        {
            using (var key = (RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default)).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
                return (int?)key?.GetValue("Release") ?? 0;
        }
    }
}
