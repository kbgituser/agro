using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]        
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EntryDate { get; set; }
        [Display(Name = "Удаленный")]
        public bool IsDeleted { get; set; }

        public BaseEntity(DateTime entryDate, bool isDeleted)
        {
            EntryDate = entryDate;
            IsDeleted = isDeleted;
        }
        public BaseEntity()
        { EntryDate = DateTime.Now; }
    }
}