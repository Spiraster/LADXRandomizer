using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    [Flags]
    public enum Settings
    {
        None,
        DebugMode               = 0x1,
        ShuffleWarps            = 0x2,
        PairWarps               = 0x4,
        ExcludeHouse            = 0x8,
        ExcludeEgg              = 0x10,
        ShuffleItems            = 0x20,
        HouseWarp               = 0x40,
        PatchWarpSaving         = 0x80,
        PatchSignpostMaze       = 0x100,
        PatchEggMaze            = 0x200,
        PatchGhost              = 0x400,
        PatchD0Entrance         = 0x800,
        PatchSlimeKey           = 0x1000,
        PatchWaterfalls         = 0x2000,
        PatchTrendy             = 0x4000,
        DisableBowwowKids       = 0x8000,
        DisableBirdKeyPits      = 0x10000,
        DisableLanmolasPit      = 0x20000,
        CoverD7Pit              = 0x40000,
        PreventWaterSoftlocks   = 0x80000,
        RemoveHouseMarin        = 0x100000,
        RemoveOwls              = 0x200000,
        Max                     = 0x400000
    }

    public enum SettingPreset
    {
        Standard = 0x28FDDE,
    }
}
