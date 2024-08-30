

using System.ComponentModel.DataAnnotations;
using Agro.Model.Enums;

namespace Agro.Model.Entities
{
    public class Offer: BaseEntity
    {
        //public Offer()
        //{
        //    this.OfferStatus = OfferStatus.Created;
        //}
        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Запрос")]
        public int RequestId { get; set; }
        [Display(Name = "Запрос")]
        public virtual Intention Request { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Можно вводить только цифры")]

        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        
        [Display(Name = "Статус")]
        public OfferStatus OfferStatus { get; set; }
    }
}