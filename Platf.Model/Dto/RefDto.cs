using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Dto
{
    public class RefDto : BaseDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
    }
}
