using System.Collections.Generic;

namespace Agro.Model.Dto.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public int? ParentCategoryId { get; set; }
        
        //public virtual ICollection<int> ChildrenCategories { get; set; }
        //public virtual ICollection<Reques> Requests { get; set; }
    }
}