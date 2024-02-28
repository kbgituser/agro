using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Enums
{
    public enum RequestStatus
    {
        [Description("Созданый")]
        Created = 0,
        [Description("На модерации")]
        InModeration = 1,
        [Description("Активный")]
        Active = 2,
        [Description("Выбор предложений")]
        Selection = 3,
        [Description("Выбран")]
        Selected = 4,
        [Description("Отказаный")]
        Rejected = 5,
        [Description("Выполненный")]
        Done = 6,
        [Description("Закрытый")]
        Closed = 7
    }
}
