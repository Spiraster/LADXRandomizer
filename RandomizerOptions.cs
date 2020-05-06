using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class RandomizerOptions
    {
        public OptionList List = new OptionList
        {
            new Option { Name = "SelectedROM", Type = typeof(ComboBox) },
            new Option { Name = "IncludeMarinHouse" },
            new Option { Name = "IncludeEgg" },
            new Option { Name = "PreventDefaultWarps" },
            new Option { Name = "PreventInaccessible" },
        };
    }

    public class Option
    {
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

    public class OptionList : List<Option>
    {
        public Option this[string name]
        {
            get { return this.First(w => w.Name == name); }
        }
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
