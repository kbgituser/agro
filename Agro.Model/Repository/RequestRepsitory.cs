using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro.Model.Repository
{
    public class RequestRepsitory : AsyncRepository<Request>, IRequestRepository
    {
        public RequestRepsitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Request> GetAll()
        {
            return _dbContext.Requests.Include(r => r.User);
        }

        public async Task<PaginatedList<Request>> GetAllRequestsPagedAsync(int? p)
        {
            var t = _dbContext.Requests.Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }

        public async Task<Request> GetRequestById(int id)
        {
            return  await _dbContext.Requests.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r=>r.Intentions)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p)
        {
            var t = _dbContext.Requests.Where(x => x.Status == status).Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }

        public async void GroupIntentionsToRequest()
        {
            var allIntentions = _dbContext.Intentions.OrderBy(x => x.EntryDate);
            var wholeIntentions = allIntentions.Where(x => x.AnimalPart == Enums.AnimalPart.Whole);
            List<Request> requests = new List<Request>();
            foreach (var wholeIntention in wholeIntentions)
            {
                var request = new Request();
                request.Intentions.Add(wholeIntention);
                requests.Add(request);
            }

            int elementsInPart = (int)Enums.AnimalPart.Half;

            var halfIntentions = allIntentions.Where(x => x.AnimalPart == Enums.AnimalPart.Half);
            int halfsGroupCount = halfIntentions.Count() / elementsInPart;
            for (int i = 0; i < halfsGroupCount; i += elementsInPart)
            {
                var request = new Request();
                request.Intentions.Add(halfIntentions.ElementAt(i));
                request.Intentions.Add(halfIntentions.ElementAt(i + 1));
                requests.Add(request);
            }

            elementsInPart = (int)Enums.AnimalPart.Third;
            var thirdIntentions = allIntentions.Where(x => x.AnimalPart == Enums.AnimalPart.Third);
            int thirdGroupCount = thirdIntentions.Count() / elementsInPart;
            for (int i = 0; i < thirdGroupCount; i += elementsInPart)
            {
                var request = new Request();
                request.Intentions.Add(thirdIntentions.ElementAt(i));
                request.Intentions.Add(thirdIntentions.ElementAt(i + 1));
                request.Intentions.Add(thirdIntentions.ElementAt(i + 2));
                requests.Add(request);
            }

            elementsInPart = (int)Enums.AnimalPart.Forth;
            var forthIntentions = allIntentions.Where(x => x.AnimalPart == Enums.AnimalPart.Third);
            int forthGroupCount = forthIntentions.Count() / elementsInPart;
            for (int i = 0; i < forthGroupCount; i += elementsInPart)
            {
                var request = new Request();
                request.Intentions.Add(forthIntentions.ElementAt(i));
                request.Intentions.Add(forthIntentions.ElementAt(i + 1));
                request.Intentions.Add(forthIntentions.ElementAt(i + 2));
                request.Intentions.Add(forthIntentions.ElementAt(i + 3));
                requests.Add(request);
            }
            _dbContext.Requests.AddRange(requests);            
        }
        public bool AssignToPerformer(int id, string userId)
        {
            var request = _dbContext.Requests.FirstOrDefault(x => x.Id == id);
            if (request is null)
            {
                return false;
            }
            request.Status = RequestStatus.Selected;
            request.UserId = userId;
            return true;
        }
        public bool Release(int id, string userId)
        {
            var request = _dbContext.Requests.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            
            if (request is null)
            {
                return false;
            }

            request.UserId = "";
            request.Status= RequestStatus.Active;
            return true;
        }
    }
}
