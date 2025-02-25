using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;

namespace Agro.Model.Repository;

public class CityRepository: AsyncRepository<City>, ICityRepository
  {
      public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
      {
          _dbContext = dbContext;
      }
  }
