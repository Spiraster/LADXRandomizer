using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class RandomizerSettings : List<Setting>
    {
        private int mask;
        public int Mask => mask;

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
                new Setting { Name = "ExcludeMarinHouse" },
                new Setting { Name = "ExcludeEgg" },
                
                new Setting { Name = "PatchWarpSaving", Enabled = true },
                new Setting { Name = "PatchSignpostMaze", Enabled = true },
                new Setting { Name = "PatchEggMaze", Enabled = false },
                new Setting { Name = "PatchGhost", Enabled = true },
                new Setting { Name = "PatchSlimeKey", Enabled = true },
                new Setting { Name = "DisableBowwowKids", Enabled = true },
                new Setting { Name = "DisableBirdKeyPits" },
                new Setting { Name = "DisableLanmolasPit" },
                new Setting { Name = "CoverD7Pit", Enabled = true },
                new Setting { Name = "RemoveHouseMarin", Enabled = false },
                new Setting { Name = "RemoveOwls", Enabled = true },
                new Setting { Name = "SafetyWarp", Enabled = true },

                new Setting { Name = "RandomizeWarps", ShowInLog = false, Enabled = true },
                new Setting { Name = "DebugMode", ShowInLog = false, Enabled = false },
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
        ExcludeMarinHouse   = 128,
        ExcludeEgg          = 256,
    }

    public enum Preset
    {
        Standard = 441,
        //Hard     = xx,
        Chaos    = 385,
    }

    public enum Rom
    {
        J10,
        U10,
        U12
    }
}
