using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Enums
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
