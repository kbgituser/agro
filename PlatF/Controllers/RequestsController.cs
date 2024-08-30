//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using PlatF.Model.Data;
//using PlatF.Model.Entities;
//using PlatF.Model.Enums;
//using PlatF.Model.Interfaces;
//using PlatF.Helpers;
//using Microsoft.AspNetCore.Authorization;
//using PlatF.PaginatedList;
//using Logic.Services;
//using PlatF.Model.Dto.Request;
//using AutoMapper;

//namespace PlatF.Controllers
//{
//    public class RequestsController : Controller
//    {
//        private readonly RequestService _requestService;
//        private readonly ApplicationDbContext _context;
//        private UserManager<ApplicationUser> _userManager;
//        private RoleManager<IdentityRole> _roleManager;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IMapper _mapper;
//        private IUnitOfWork _unitOfWork;

//        public RequestsController(ApplicationDbContext context
//            , UserManager<ApplicationUser> userManager
//            , RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork
//            , IHttpContextAccessor httpContextAccessor
//, RequestService requestService, IMapper mapper)
//        {
//            _context = context;
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _unitOfWork = unitOfWork;
//            _httpContextAccessor = httpContextAccessor;
//            _requestService = requestService;
//            _mapper = mapper;
//        }

//        // GET: Requests
//        public async Task<IActionResult> Index(int? p)
//        {
//            //var requests = _context.Requests.Include(r => r.Category).Include(r => r.User).Include(x=>x.City);

//            var requests = await _requestService.GetRequestsByStatusPagedAsync(RequestStatus.Active, p);

//            //var requests = _unitOfWork.RequestRepository.GetAll();
//            ViewBag.Statuses = EnumExtHelper.GetValues<RequestStatus>();

//            int pageSize = 10;
//            int pN = (p ?? 1);

//            ViewBag.PageNo = pN;
//            ViewBag.PageSize = pageSize;
//            ViewBag.TotalRecords = requests.Count();
            
//            return View(requests);
//        }

//        [Authorize]
//        public async Task<IActionResult> MyRequests(int? p)
//        {
//            var requests = _context.Requests.
//                Where(x=>x.UserId == UserHelper.GetCurrentUser(_httpContextAccessor, _context).Id)
//                .Include(r => r.Category).Include(r => r.User).Include(x=>x.City);
        
//            int pageSize = 10;
//            int pN = (p ?? 1);

//            ViewBag.PageNo = pN;
//            ViewBag.PageSize = pageSize;
//            ViewBag.TotalRecords = requests.Count();
//            return View(await PaginatedList<Request>.CreateAsync(requests.OrderByDescending(x => x.StartDate).AsNoTracking(), pN, pageSize));            
//        }

//        [Authorize(Roles ="Admin")]
//        public async Task<IActionResult> RequestsForModeration(int? p)
//        {
//            var requests = _context.Requests.
//                Where(x => x.UserId == UserHelper.GetCurrentUser(_httpContextAccessor, _context).Id)
//                .Include(r => r.Category).Include(r => r.User).Include(x => x.City);

//            ViewBag.Statuses = EnumExtHelper.GetValues<RequestStatus>();

//            int pageSize = 10;
//            int pN = (p ?? 1);

//            ViewBag.PageNo = pN;
//            ViewBag.PageSize = pageSize;
//            ViewBag.TotalRecords = requests.Count();
//            return View(requests);
//        }

//        // GET: Requests/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            //var request = await _context.Requests
//            //    .Include(r => r.Category)
//            //    .Include(r => r.User)
//            //    .Include(r=>r.City)
//            //    .FirstOrDefaultAsync(m => m.Id == id);
//            var request = await _requestService.GetRequestByIdAsync(id.Value);

//            if (request == null)
//            {
//                return NotFound();
//            }

//            return View(request);
//        }

//        // GET: Requests/Create
//        [Authorize]
//        public IActionResult Create()
//        {
//            var request = new Request();
//            request.Price = 1;
//            request.StartDate = DateTime.Today;
//            request.EndDate = DateTime.Today.AddDays(15);
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
//            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
//            return View(request);
//        }

//        // POST: Requests/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize]
//        public async Task<IActionResult> Create([Bind("Title,CityId,Price,Message,CreateDate,StartDate,EndDate,RequestStatus,CategoryId,Id,EntryDate,IsDeleted")] Request request)
//        {
//            ModelState.Remove("CategoryId");
//            if (ModelState.IsValid)
//            {
//                request.UserId = GetCurrentUser()?.Id;
//                if (request.UserId == null)
//                {
//                    return View(request);
//                }

//                _unitOfWork.RequestRepository.Add(request);
//                await _unitOfWork.Commit();
//                //_context.Add(request);                
//                //await _context.SaveChangesAsync();



//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", request.CategoryId);
//            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", request.UserId);
            

//            return View(request);
//        }

//        // GET: Requests/Edit/5
//        [Authorize]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var request = await _context.Requests.FindAsync(id);
//            if (request == null)
//            {
//                return NotFound();
//            }

//            var currentUser = UserHelper.GetCurrentUser(_httpContextAccessor, _context);
//            await _requestService.IsUsersRequest(request.Id, currentUser.Id);
//            if (request.UserId != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
//            {
//                TempData["ErrorMessage"] = "У вас нет прав редактировать заявку";
//                return RedirectToAction ("Error","Home");
//            }
//            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", request.CategoryId);
//            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "Name", request.City);
//            ViewData["CanEdit"] = request.RequestStatus == RequestStatus.Created;
//            return View(request);
//        }

//        // POST: Requests/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Title,UserId,CityId,Price,Message,CreateDate,StartDate,EndDate,RequestStatus,CategoryId,Id,EntryDate,IsDeleted")] RequestDto requestDto)
//        {
//            var request = _mapper.Map<RequestDto>(requestDto);
//            if (id != request.Id)
//            {
//                return NotFound();
//            }            
//            ModelState.Remove("CategoryId");
//            if (ModelState.IsValid)
//            {
//                try
//                {

//                    //_context.Update(request);
//                    //_context.Entry(request).Property(x => x.EntryDate).IsModified = false;
//                    //_context.Entry(request).Property(x => x.RequestStatus).IsModified = false;
//                    //_context.Entry(request).Property(x => x.UserId).IsModified = false;
//                    //await _context.SaveChangesAsync();

//                    await _requestService.Update(request);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!RequestExists(request.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", request.CategoryId);
//            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "Name", request.CityId);
//            ViewData["CanEdit"] = request.RequestStatus == RequestStatus.Created;
//            return View(request);
//        }

//        // GET: Requests/Delete/5
//        [Authorize]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var request = await _context.Requests
//                .Include(r => r.Category)
//                .Include(r => r.User)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (request.UserId != UserHelper.GetCurrentUser(_httpContextAccessor, _context).Id)            
//            {
//                TempData["ErrorMessage"] = "У Вас нет прав для редактирования заявки";
//                return RedirectToAction("Error");
//            }
//            if (request == null)
//            {
//                return NotFound();
//            }

//            return View(request);
//        }

//        public async Task<IActionResult> SendToModeration(int id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var request = await _context.Requests.FindAsync(id);
//            if (request == null)
//            {
//                return NotFound();
//            }
//            request.RequestStatus = RequestStatus.InModeration;
//            _context.Attach(request);
//            await _context.SaveChangesAsync();            
//            return RedirectToAction(
//                nameof(Details)
//                , new { id = request.Id }
//                );
//            //return View(request);
//        }

//        // POST: Requests/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var request = await _context.Requests.FindAsync(id);
            
//            if (request.UserId != UserHelper.GetCurrentUser(_httpContextAccessor, _context).Id)
//            {
//                TempData["ErrorMessage"] = "У Вас нет прав для редактирования заявки";
//                return RedirectToAction("Error");
//            }

//            _context.Requests.Remove(request);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool RequestExists(int id)
//        {
//            return _context.Requests.Any(e => e.Id == id);
//        }

//        public ApplicationUser GetCurrentUser()
//        {
//            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
//            if (user != null)
//            {
//                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//                if (userId != null)
//                {
//                    var apuser = _context.ApplicationUsers.SingleOrDefault(u => u.Id == userId);
//                    return apuser;
//                }
//            }

//            return null;
//        }
//    }
//}
