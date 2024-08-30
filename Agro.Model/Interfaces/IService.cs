using Agro.Model.Dto.City;
using Agro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        public T Create(T Dto);
        public T Update(T Dto);
        public void Delete(T Dto);
        public T GetByIdAsync(int id);
        public void DeleteById(int id);        
    }
}
