using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        //[Display(Name = "Почта")]
        //public string Email { get; set; }        
        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Блокированный")]
        public bool Blocked { get; set; }

        [Display(Name = "Подтвержденный")]
        public bool EmailConfirmed { get; set; }
    }
}
