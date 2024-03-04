using System.ComponentModel;

namespace PlatF.Model.Enums
{
    public enum IntentionStatus
    {
        //[Description("Созданый")]
        //Created = 0,
        //[Description("На модерации")]
        //InModeration = 1,
        [Description("Активный")]
        Active = 2,
        //[Description("Выбор предложений")]
        //Selection = 3,
        [Description("Выбран")]
        Selected = 4,
        //[Description("Отказаный")]
        //Rejected = 5,
        [Description("В работе")]
        InProcess = 6,
        [Description("Выполненный")]
        Done = 6,
        [Description("Закрытый")]
        Closed = 7
    }
}
