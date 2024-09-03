﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Dto
{
    public class UserForRegistrationDto
    {

        //[Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(12)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
