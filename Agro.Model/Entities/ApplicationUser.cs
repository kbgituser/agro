using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Блокированный")]
        public bool Blocked { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Город")]
        public int? CityId { get; set; }
        [Display(Name = "Город")]
        public virtual City City { get; set; }


        //[EmailAddress(ErrorMessage = "Укажите корректный Email.")]
        ////[EmailAddress(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "EmailAddress")]
        //public new string Email { get; set; }
    }
}
