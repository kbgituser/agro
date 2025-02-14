using Agro.Model.Entities;
using Agro.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Dto.Intention
{
    public class IntentionDto: RefDto
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите пожалуйста заголовок")]
        public virtual string Name { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Город")]
        public int? CityId { get; set; }

        public Entities.City City { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        //[Required(ErrorMessage ="Введите описание к запросу")]
        public string Message { get; set; }

        [Display(Name = "Статус")]
        public IntentionStatus IntentionStatus { get; set; }
        [Display(Name = "Часть скота")]

        public AnimalPart AnimalPart { get; set; }
    } 
}
