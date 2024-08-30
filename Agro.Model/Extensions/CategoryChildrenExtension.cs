using Agro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Extensions
{
    public static class CategoryChildrenExtension
    {
        public static IEnumerable<Category> Descendants(this Category category)
        {
            return category.ChildrenCategories.Concat(category.ChildrenCategories.SelectMany(n => n.Descendants()));
        }
    }
}
