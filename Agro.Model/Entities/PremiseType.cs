using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class PremiseType: RefEntity
    {        
        [Display(Name = "Наименование типа помещения")]
        [Required(ErrorMessage = "Наименование типа обязательно")]
        public override string Name { get; set; }
    }
}
