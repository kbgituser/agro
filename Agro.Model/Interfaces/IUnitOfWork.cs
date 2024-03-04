using Microsoft.AspNetCore.Identity;
using PlatF.Model.Entities;
using System.Threading.Tasks;

namespace PlatF.Model.Interfaces
{
    public interface IUnitOfWork
    {
        IAdvertisementRepository AdvertisementRepository { get; }

        ICategoryRepository CategoryRepository { get; }
        ICityRepository CityRepository { get; }

        IIntentionRepository IntentionRepository { get; }
        IRequestRepository RequestRepository { get; }

        IOfferRepository OfferRepository { get; }
        UserManager<ApplicationUser> UserManager{get;}
        Task Commit();
        Task RejectChanges();
        void Dispose();
    }
}
