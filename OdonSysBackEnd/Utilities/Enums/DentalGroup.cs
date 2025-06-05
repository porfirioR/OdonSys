using System.ComponentModel;

namespace Utilities.Enums;

public enum DentalGroup
{
    [Description("Incisivos")]
    Incisors = 1,
    [Description("Caninos")]
    Canines = 2,
    [Description("Premolares")]
    Premolars = 3,
    [Description("Molares")]
    Molars = 4
}
