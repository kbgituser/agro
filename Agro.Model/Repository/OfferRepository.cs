using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;

namespace Agro.Model.Repository;

public class OfferRepository: AsyncRepository<Offer>, IOfferRepository
  {
      public OfferRepository(ApplicationDbContext dbContext) : base(dbContext)
      {
          _dbContext = dbContext;
      }
  }
