using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public class Premise: RefEntity
    {
        public Premise()
        {
            IsSeen = true;
            HasWindow = true;
        }
        
        [Display(Name = "Номер помещения")]
        public string Number { get; set; }
        [Display(Name = "Этаж")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Можно вводить только цифры")]
        public int Floor { get; set; }
        [Display(Name = "Торговый центр")]
        public int? MallId { get; set; }
        [Display(Name = "Торговый центр")]
        public virtual Mall Mall { get; set; }        

        [RegularExpression("([1-9][0-9]*.?[0-9]*)", ErrorMessage = "Можно вводить только целое или дробное число")]
        [Display(Name = "Площадь")]
        public double Area { get; set; }
        [Display(Name = "Последний этаж?")]
        public bool IsLastFloor { get; set; }
        public string IsLastFloorString
        {
            get { return IsLastFloor ? "Да" : "Нет"; }
        }
        [Display(Name = "Есть ли окно?")]
        public bool HasWindow { get; set; }
        public string HasWindowString
        {
            get { return HasWindow ? "Да" : "Нет"; }
        }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }        

        [Display(Name = "Фотография с инстаграмма")]
        [DataType(DataType.MultilineText)]        
        public string InstaPhoto
        {
            get; set;
        }

        
        [Display(Name = "Фотографии")]
        public virtual ICollection<PremisePhoto> PremisePhotos { get; set; }
        
        [Display(Name = "Видимость")]
        public bool IsSeen { get; set; }
        
        [Display(Name = "Тип помещения")]
        public int? PremiseTypeId { get; set; }
        [Display(Name = "Тип помещения")]
        public virtual PremiseType PremiseType { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }
        private int cityId;
        [Required(ErrorMessage = "Выберите город")]
        [Display(Name = "Город")]
        public int CityId {
            //get
            //{

            //    if (!this.MallId.HasValue)
            //    {
            //        return cityId;
            //    }
            //    else
            //    {                    
            //        return (int)this.Mall.CityId;
            //    }
            //}

            //set
            //{ cityId = value; }
            get;set;
        }

        [Display(Name = "Город")]
        public virtual City City { get; set; }

        public string FirstPhoto()
        {
            if (PremisePhotos.Any())
            {
                return PremisePhotos.FirstOrDefault().Path;
            }
            return "";
        }
    }
}
