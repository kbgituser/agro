using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Repository
{
    public class OfferRepository: AsyncRepository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
