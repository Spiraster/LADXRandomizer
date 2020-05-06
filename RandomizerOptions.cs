using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class RandomizerOptions : List<Option>
    {
        public Option this[string name]
        {
            get { return this.First(w => w.Name == name); }
        }

        public RandomizerOptions()
        {
            this.AddRange(new List<Option>
            {
                new Option { Name = "SelectedROM", Type = typeof(ComboBox) },
                new Option { Name = "IncludeMarinHouse" },
                new Option { Name = "IncludeEgg" },
                new Option { Name = "PreventDefaultWarps" },
                new Option { Name = "PreventInaccessible" },
                new Option { Name = "CoverPitWarp", ShowInLog = false, Enabled = true },
                new Option { Name = "DebugMode", ShowInLog = false, Enabled = true },
            });
        }
    }

    public class Option
    {
        public bool ShowInLog { get; set; } = true;

        public string Name { get; set; }
        public bool Enabled { get; set; } = false;
        public int Index { get; set; }
        public Type Type { get; set; } = typeof(bool);
    }

    public enum Rom
    {
        J10,
        U10,
        U12
    }

    public class OptionsArgs : EventArgs
    {
        public RandomizerOptions Options { get; }

        public OptionsArgs(RandomizerOptions options)
        {
            Options = options;
        }
    }
}
