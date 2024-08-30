using Agro.Model.Entities;
using Agro.Model.Repository;
using System.Collections.Generic;

namespace Agro.Model.Interfaces
{
    public interface ICategoryRepository: IAsyncRepository<Category>
    {
        
    }
}