using System;
using System.ComponentModel.DataAnnotations;

namespace Agro.Model.Entities
{
    public abstract class RefEntity: BaseEntity
    {
        [Display(Name = "Наименование")]
        public virtual string Name { get; set; }
        [Display(Name = "Активный")]
        public bool IsActive { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        public RefEntity(DateTime entryDate, bool isDeleted, string name, bool isActive, string code): base(entryDate, isDeleted)
        {
            Name = name;
            IsActive = isActive;
            Code = code;
        }
        public RefEntity():base()
        {
            IsActive = true;
        }
    }
}
