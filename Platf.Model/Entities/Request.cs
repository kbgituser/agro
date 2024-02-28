using PlatF.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public class Request: RefEntity
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите пожалуйста заголовок")]
        public string Title { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }


        [Display(Name = "Город")]
        public int? CityId { get; set; }
        [Display(Name = "Город")]
        public virtual City City { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Можно вводить только цифры")]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Введите описание к запросу")]
        public string Message { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }        

        [Display(Name = "Дата начала приема предложений")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата окончания приема предложений")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Статус")]
        public RequestStatus RequestStatus { get; set; }
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        [Display(Name = "Категория")]
        public Category Category { get; set; }
    }
}
