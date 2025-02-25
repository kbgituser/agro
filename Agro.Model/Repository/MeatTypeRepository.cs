using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;

namespace Agro.Model.Repository
{
  public class MeatTypeRepository: AsyncRepository<MeatType>, IMeatTypeRepository
  {
    public MeatTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }
  }
}
