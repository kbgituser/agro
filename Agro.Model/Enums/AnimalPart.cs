using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Enums;

public enum AnimalPart
{
    [Description("Целиком")]
    [Display(Name = "Целиком")]
    Whole = 1,
    
    [Description("Половина")]
    [Display(Name = "Половина")]
    Half = 2,
    
    [Description("Треть")]
    [Display(Name = "Треть")]
    Third = 3,

    [Description("Четверть")]
    [Display(Name = "Четверть")]
    Forth = 4,
}
