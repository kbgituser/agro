using Microsoft.AspNetCore.Identity;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;
using PlatF.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Interfaces
{
    public interface IUnitOfWork
    {
        IAdvertisementRepository AdvertisementRepository { get; }

        ICategoryRepository CategoryRepository { get; }
        ICityRepository CityRepository { get; }

        IRequestRepository RequestRepository { get; }
        IOfferRepository OfferRepository { get; }
        UserManager<ApplicationUser> UserManager{get;}
        Task Commit();
        Task RejectChanges();
        void Dispose();
    }
}
