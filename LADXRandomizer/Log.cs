using System;
using System.IO;
using System.Text;

namespace LADXRandomizer
{
    public class Log
    {
        private string header;
        private string spoilerLog;
        private string pathfinderLog;

        public event EventHandler<LogArgs> UpdateLog;

        public Log(string seed, Settings settings)
        {
            header += "Seed: " + seed + "\r\n";
            header += "Settings: " + Base62.ToBase62((uint)settings) + "\r\n\r\n";
        }

        public void Print(params string[] messages) => UpdateLog?.Invoke(this, new LogArgs(Parse(messages)));

        public void Record(LogMode logMode, params string[] messages)
        {
            if (logMode == LogMode.Debug && Program.Debug)
                Print(messages);
            else if (logMode == LogMode.Spoiler)
                spoilerLog += Parse(messages);
            else if (logMode == LogMode.Pathfinder)
                pathfinderLog += Parse(messages);
        }

        private string Parse(string[] messages)
        {
            var sb = new StringBuilder();

            foreach (string message in messages)
            {
                if (message == "<l1>")
                    sb.AppendLine("================================================");
                else if (message == "<l2>")
                    sb.AppendLine("--------------------------------------");
                else
                    sb.AppendLine(message);
            }

            return sb.ToString();
        }

        public void RecordWarps(WarpList warpList, Settings settings)
        {
            Record(LogMode.Spoiler, "<l1>", "Warps:", "<l1>");

            foreach (var warp1 in warpList.Overworld1)
            {
                var warp2 = warp1.GetDestinationWarp();

                string text1 = "[" + warp1.Code + "] " + warp1.Description;

                string text2 = "";
                if (warp2 != null)
                    text2 = "[" + warp2.Code + "] " + warp2.Description;

                Record(LogMode.Spoiler, text1 + "\r\n    ^=> " + text2 + "\r\n");
            }

            if (!settings.HasFlag(Settings.PairWarps))
            {
                foreach (var warp1 in warpList.Overworld2)
                {
                    var warp2 = warp1.GetDestinationWarp();

                    string text1 = "[" + warp1.Code + "] " + warp1.Description;

                    string text2 = "";
                    if (warp2 != null)
                        text2 = "[" + warp2.Code + "] " + warp2.Description;

                    Record(LogMode.Spoiler, text1 + "\r\n    ^=> " + text2 + "\r\n");
                }
            }
        }

        public void Save(string filename)
        {
            var output = header + spoilerLog;
            if (Program.Debug)
                output += pathfinderLog;

            var path = "Output\\Log_" + filename + ".txt";

            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            File.WriteAllText(path, output);
        }
    }

    public class LogArgs : EventArgs
    {
        public string Message { get; }
        
        public LogArgs(string msg) => Message = msg;
    }

    public enum LogMode
    {
        Debug,
        Spoiler,
        Pathfinder,
    }
}
