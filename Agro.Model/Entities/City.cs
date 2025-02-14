using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Entities
{
    public class City : RefEntity
    {
        public City():base()
        {
        }

        [Display(Name = "Наименование города")]
        public override string Name { get; set; }
        public virtual ICollection<Intention> Requests { get; set; }
    }
}
