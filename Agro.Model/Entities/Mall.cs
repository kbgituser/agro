using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class Mall: RefEntity
    {
        public Mall()
        {
            ParkingExists = true;
            ParkingInsideExists = true;
            ParkingPayment = true;
            ParkingInsidePayment = true;
        }
        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        [Display(Name = "Наименование центра")]
        [Required(ErrorMessage = "Наименование центра обязательно")]
        public override string Name { get; set; }
        [Display(Name = "Адрес центра")]
        public string Address { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Помещения")]
        public virtual ICollection<Premise> Premises { get; set; }
        [Display(Name = "Количество этажей")]
        public int NumberOfFloors { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Есть ли парковка")]
        public bool ParkingExists { get; set; }
        [Display(Name = "Есть ли паркинг")]
        public bool ParkingInsideExists { get; set; }
        [Display(Name = "Платная ли парковка")]
        public bool ParkingPayment { get; set; }
        [Display(Name = "Платный ли паркинг")]
        public bool ParkingInsidePayment { get; set; }

        [Display(Name = "Фотографии")]
        public virtual ICollection<MallPhoto> MallPhotos { get; set; }

        //[Required(ErrorMessage = "Выберите город")]
        [Display(Name = "Город")]
        public int? CityId { get; set; }

        [Display(Name = "Город")]
        public virtual City City { get; set; }

        [Display(Name = "Цена за кватратный метр")]
        public int Smprice { get; set; }
    }
}
