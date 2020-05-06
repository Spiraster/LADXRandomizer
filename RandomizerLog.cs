using System;
using System.Text;

namespace LADXRandomizer
{
    public class RandomizerLog
    {
        private bool debug;
        private string info;
        private string settings;
        private string spoiler;

        //public string InfoLog { get { return info; } }
        //public string SpoilerLog { get { return spoiler; } }
        public string FullLog { get { return info + settings + spoiler; } }

        public event EventHandler<LogArgs> UpdateLog;

        public RandomizerLog(bool debug = false)
        {
            this.debug = debug;
        }

        public void Write(LogMode mode, params string[] messages)
        {
            var sb = new StringBuilder();
            bool clear = false;

            foreach (string message in messages)
            {
                if (message == "<clear>")
                    clear = true;
                else if (message == "<l1>")
                    sb.AppendLine("=====================================================================");
                else if (message == "<l2>")
                    sb.AppendLine("--------------------------------------");
                else
                    sb.AppendLine(message);
            }

            if (mode == LogMode.Info)
            {
                info += sb.ToString();
                UpdateLog?.Invoke(this, new LogArgs(sb.ToString(), clear));
            }
            else if (mode == LogMode.Debug && debug)
            {
                info += sb.ToString();
                UpdateLog?.Invoke(this, new LogArgs(sb.ToString(), clear));
            }
            else if (mode == LogMode.Settings)
                settings += sb.ToString();
            else if (mode == LogMode.Spoiler)
                spoiler += sb.ToString();
        }

        public void LogSettings(RandomizerOptions options)
        {
            Write(LogMode.Settings, "<l2>", "Settings:", "<l2>");
            Write(LogMode.Settings, options["SelectedROM"].Name + " = " + (Rom)options["SelectedROM"].Index);
            foreach (var option in options)
            {
                if (!option.ShowInLog && !debug)
                    continue;
                else if (option.Type == typeof(bool))
                    Write(LogMode.Settings, option.Name + " = " + option.Enabled.ToString().ToUpper());
            }
        }
    }

    public class LogArgs : EventArgs
    {
        public string Message { get; }
        public bool doClear { get; }
        
        public LogArgs(string msg, bool clear)
        {
            Message = msg;
            doClear = clear;
        }
    }

    public enum LogMode
    {
        Info,
        Settings,
        Spoiler,
        Debug,
    }
}
