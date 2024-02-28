using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public class City : RefEntity
    {
        public City():base()
        {
        }

        [Display(Name = "Наименование города")]
        public override string Name { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
