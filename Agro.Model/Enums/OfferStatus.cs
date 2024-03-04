using System.ComponentModel;

namespace PlatF.Model.Enums
{
    public enum OfferStatus
    {
        [Description("Созданый")]
        Created = 0,
        [Description("Активный")]
        Active = 1,
        [Description("Отказаный")]
        Rejected = 2,
        [Description("Выбранный")]
        Selected = 3,
        [Description("Выполненный")]
        Done = 4
    }
}
