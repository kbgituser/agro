using Agro.Model.Entities;
using PlatF.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlatF.Model.Entities
{
    public class Intention: RefEntity
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите пожалуйста заголовок")]
        public virtual string Name { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Город")]
        public int? CityId { get; set; }
        [Display(Name = "Город")]
        public virtual City City { get; set; }
        public int? RequestId { get; set; }
        public virtual Request Request { get; set; }

        //[DisplayFormat(DataFormatString = "{0:#,#}")]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Можно вводить только цифры")]
        //[Display(Name = "Цена")]
        //public double Price { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        //[Required(ErrorMessage ="Введите описание к запросу")]
        public string Message { get; set; }

        [Display(Name = "Статус")]
        public IntentionStatus IntentionStatus { get; set; }
    }
}
