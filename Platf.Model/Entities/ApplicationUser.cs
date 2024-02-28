using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Блокированный")]
        public bool Blocked { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        //[EmailAddress(ErrorMessage = "Укажите корректный Email.")]
        ////[EmailAddress(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "EmailAddress")]
        //public new string Email { get; set; }
    }
}
