using System.ComponentModel;

namespace Agro.Model.Enums
{
    public enum UserStatus
    {
        [Description("Созданый")]
        Created = 0,
        [Description("Данные подтверждены")]
        DataApproved = 1,
        [Description("Оплачено")]
        Payed = 2
    }
}
