using System;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Dto.UserRegistration
{
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [MaxLength(11)]
        [MinLength((11),ErrorMessage ="Минимальное количество цифр 11")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Только цифры")]
        //[Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
        //[StringLength(20, MinimumLength = 11, ErrorMessage = "Contact number should have minimum 11 digits")]
        [Display(Name ="Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
