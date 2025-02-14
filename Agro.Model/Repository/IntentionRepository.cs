using System.Linq;
using System.Threading.Tasks;
using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using Microsoft.EntityFrameworkCore;

namespace Agro.Model.Repository;

public class IntentionRepository : AsyncRepository<Intention>, IIntentionRepository
{
    public IntentionRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Intention> GetAll()
    {
        return _dbContext.Intentions.Include(r => r.User).Include(r => r.City);
    }

    public async Task<PaginatedList<Intention>> GetAllIntentionsPagedAsync(int? p)
    {
        var t = _dbContext.Intentions.Include(r => r.User).Include(x => x.City);
        int pageSize = 10;
        int pN = (p ?? 1);
        return await PaginatedList<Intention>.CreateAsync(
            t.OrderByDescending(x => x.EntryDate).AsNoTracking(),
            pN,
            pageSize
        );
    }
    
    public async Task<PaginatedList<Intention>> GetIntentionByUserId(string userId, int? p)
    {
        int pageSize = 10;
        int pN = (p ?? 1);
        var intentionsByUser = _dbContext
            .Intentions.AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.City)
            .Include(r => r.Request)
            .Where(x => x.UserId == userId);

        return await PaginatedList<Intention>.CreateAsync(
            intentionsByUser.OrderByDescending(x => x.EntryDate).AsNoTracking(),
            pN,
            pageSize
        );
    }

    public async Task<Intention> GetIntentionById(int id)
    {
        return await _dbContext
            .Intentions.AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.City)
            .Include(r => r.Request)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PaginatedList<Intention>> GetIntentionsByStatusPagedAsync(
        Enums.IntentionStatus status,
        int? p
    )
    {
        var t = _dbContext
            .Intentions.Where(x => x.IntentionStatus == status)
            .Include(r => r.User)
            .Include(x => x.City);
        int pageSize = 10;
        int pN = (p ?? 1);
        return await PaginatedList<Intention>.CreateAsync(
            t.OrderByDescending(x => x.EntryDate).AsNoTracking(),
            pN,
            pageSize
        );
    }
}
