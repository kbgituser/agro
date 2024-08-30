using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class MallPhoto: Photo
    {        
        public int MallId { get; set; }
        [Display(Name = "Центр")]
        public Mall Mall { get; set; }        
    }
}
