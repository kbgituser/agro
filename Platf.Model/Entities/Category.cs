using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Entities
{
    public class Category: RefEntity
    {
        public Category(DateTime entryDate, bool isDeleted, string name, bool isActive, string code) : base(entryDate, isDeleted, name, isActive, code){}
        public Category() : base()
        {         }
        public virtual ICollection<Request> Requests { get; set; }
        public int? ParentCategoryId { get; set; }
        [Display(Name = "Родительская категория")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildrenCategories { get; set; }

        [Display(Name = "Категория")]

        public virtual string FullName =>
            //$"{ParentCategory?.ParentCategory?.Name + ((ParentCategory != null && ParentCategory.ParentCategory!=null) ? " ->" : "") } " +
            //$"{  ParentCategory?.Name + ((ParentCategory != null) ? " ->" : "")} " +
            //$"{Name}";
            //►→➡→⇒⇨➜➛➨➤➾⟶⟹
            $" {ParentCategory?.FullName} {(ParentCategory!=null? "➤" : "")}  {Name}";
        
        public bool IsMyDescendant(Category category)
        {
            var isDescendant = ChildrenCategories.Contains(category);
            if (isDescendant) return true;
            else if (category.ChildrenCategories !=null)
            {
                foreach (var childCategory in ChildrenCategories)
                    childCategory.IsMyDescendant(category);
            }
            return false;
        }

        //public IEnumerable<Category> Descendants()
        //{
        //    return ChildrenCategories.Concat(ChildrenCategories.SelectMany(n => n.Descendants()));
        //}
    }
}
