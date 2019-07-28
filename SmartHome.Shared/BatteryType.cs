using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.Shared
{
    /// <summary>
    /// Types of batteries that can be used by this application.
    /// </summary>
    public enum BatteryType
    {
        Alkaline,
        NiCd,
        NiMh,
        LiPo,
        LiIon
    }
}