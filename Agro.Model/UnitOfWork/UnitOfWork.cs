using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;
using PlatF.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
        private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
			_userManager = userManager;
        }

		public IAdvertisementRepository AdvertisementRepository => new AdvertisementRepository(_dbContext);

		public ICategoryRepository CategoryRepository => new CategoryRepository(_dbContext);
		public ICityRepository CityRepository => new CityRepository(_dbContext);
		public IIntentionRepository IntentionRepository => new IntentionRepository(_dbContext);
        public IRequestRepository RequestRepository => new RequestRepsitory(_dbContext);

        public IOfferRepository OfferRepository => new OfferRepository(_dbContext);
		
		public UserManager<ApplicationUser> UserManager  => _userManager;

		public async Task Commit()
		{
			await _dbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}

		public async Task RejectChanges()
		{
			foreach (var entry in _dbContext.ChangeTracker.Entries()
					.Where(e => e.State != EntityState.Unchanged))
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
					case EntityState.Modified:
					case EntityState.Deleted:
						await entry.ReloadAsync();
						break;
				}
			}
		}
	}
}
