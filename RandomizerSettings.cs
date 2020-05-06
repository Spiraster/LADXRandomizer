using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class RandomizerSettings : List<Setting>
    {
        private int mask;
        public int Mask { get { return mask; } }

        public Setting this[string name]
        {
            get { return this.FirstOrDefault(w => w.Name == name); }
        }

        public RandomizerSettings(int mask = 1)
        {
            this.mask = mask;

            AddRange(new List<Setting>
            {
                new Setting { Name = "SelectedROM", Type = typeof(ComboBox) },
                new Setting { Name = "CheckSolvability" },
                new Setting { Name = "PairWarps" },
                new Setting { Name = "PreventInaccessible" },
                new Setting { Name = "PreventDefaultWarps" },
                new Setting { Name = "IncludeMarinHouse" },
                new Setting { Name = "IncludeEgg" },

                new Setting { Name = "CoverPitWarp", ShowInLog = false, Enabled = true },
                new Setting { Name = "DebugMode", ShowInLog = false, Enabled = true },
            });

            var settingsMask = (SettingsMask)mask;
            var settings = settingsMask.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var setting in settings)
            {
                if (setting.Contains("SelectedROM"))
                {
                    this["SelectedROM"].Index = Convert.ToInt32(setting.Split('_')[1]);
                }
                else
                {
                    if (this[setting] != null)
                        this[setting].Enabled = true;
                }
            }
        }
    }

    public class Setting
    {
        public bool ShowInLog { get; set; } = true;

        public string Name { get; set; }
        public bool Enabled { get; set; } = false;
        public int Index { get; set; }
        public Type Type { get; set; } = typeof(bool);
    }

    [Flags]
    public enum SettingsMask //max is 509
    {
        None                = 0,
        SelectedROM_0       = 1, //J10
        SelectedROM_1       = 2, //U10
        SelectedROM_2       = 4, //U12
        CheckSolvability    = 8,
        PairWarps           = 16,
        PreventInaccessible = 32,
        PreventDefaultWarps = 64,
        IncludeMarinHouse   = 128,
        IncludeEgg          = 256,
    }

    public enum Preset
    {
        Standard = 57,
        //Hard     = 41,
        Chaos    = 385,
    }

    public enum Rom
    {
        J10,
        U10,
        U12
    }
}
