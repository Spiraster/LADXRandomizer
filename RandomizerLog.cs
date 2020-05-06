using System;
using System.Text;

namespace LADXRandomizer
{
    public class RandomizerLog
    {
        private string info;
        private string settings;
        private string spoiler;

        //public string InfoLog { get { return info; } }
        //public string SpoilerLog { get { return spoiler; } }
        public string FullLog { get { return info + settings + spoiler; } }

        public event EventHandler<LogArgs> UpdateLog;

        public RandomizerLog() { }

        public void Write(LogMode logMode, params string[] messages)
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

            if (logMode == LogMode.Output)
            {
                info += sb.ToString();
                UpdateLog?.Invoke(this, new LogArgs(sb.ToString(), clear));
            }
            else if (logMode == LogMode.Settings)
                settings += sb.ToString();
            else if (logMode == LogMode.Spoiler)
                spoiler += sb.ToString();
        }

        public void LogSettings(RandomizerSettings options)
        {
            Write(LogMode.Settings, "<l2>", "Settings: (Mask = " + options.Mask.ToString() + ")", "<l2>");
            Write(LogMode.Settings, options["SelectedROM"].Name + " = " + (Rom)options["SelectedROM"].Index);
            foreach (var option in options)
            {
                if (!option.ShowInLog)
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
        Output,
        Settings,
        Spoiler,
    }
}
