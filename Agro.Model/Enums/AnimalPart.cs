using System.ComponentModel;

namespace Agro.Model.Enums
{
    public enum AnimalPart
    {
        [Description("Целиком")]
        Whole = 1,
        [Description("Половина")]
        Half = 2,
        [Description("Треть")]
        Third = 3,
        [Description("Четверть")]
        Forth = 4,
    }
}
