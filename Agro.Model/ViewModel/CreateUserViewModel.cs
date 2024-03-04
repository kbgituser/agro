using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.ViewModel
{
    public class CreateUserViewModel
    {
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}
