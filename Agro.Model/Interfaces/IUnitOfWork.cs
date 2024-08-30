using Microsoft.AspNetCore.Identity;
using Agro.Model.Entities;
using System.Threading.Tasks;

namespace Agro.Model.Interfaces
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
