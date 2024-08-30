using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Repository
{
    public class CategoryRepository: AsyncRepository<Category>, ICategoryRepository
    {        
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }        
    }
}
