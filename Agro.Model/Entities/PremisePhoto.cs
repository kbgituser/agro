using PlatF.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model
{
    public class PremisePhoto: Photo
    {
        public int PremiseId { get; set; }
        [Display(Name = "Помещение")]
        public virtual Premise Premise { get; set; }        
    }
}
