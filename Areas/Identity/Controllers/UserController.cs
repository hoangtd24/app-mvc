using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnetcoremvc.Models;
using Microsoft.AspNetCore.Identity;
using aspnetcoremvc.Areas.Identity.Models.UserViewModels;

namespace aspnetcoremvc.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/User/[action]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ILogger<UserController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppMvcContext _context;
        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserController> logger, AppMvcContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

        // GET: User
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage)
        {
            var model = new UserListModel();
            model.currentPage = currentPage;

            var qr = _userManager.Users.OrderBy(u => u.UserName);

            model.totalUsers = await qr.CountAsync();
            model.countPages = (int)Math.Ceiling((double)model.totalUsers / model.ITEMS_PER_PAGE);

            if (model.currentPage < 1)
                model.currentPage = 1;
            if (model.currentPage > model.countPages)
                model.currentPage = model.countPages;

            var qr1 = qr.Skip((model.currentPage - 1) * model.ITEMS_PER_PAGE)
                        .Take(model.ITEMS_PER_PAGE)
                        .Select(u => new UserAndRole()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                        });

            model.users = await qr1.ToListAsync();

            foreach (var user in model.users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }

            return View(model);
        }

        // GET: User/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> AddRole(string id)
        {
            List<AddRoleModel> model = new List<AddRoleModel>();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var allRole = await _roleManager.Roles.ToListAsync();
            if (allRole.Count > 0)
            {

                foreach (var role in allRole)
                {
                    var addRoleModel = new AddRoleModel()
                    {
                        roleId = role.Id,
                        roleName = role.Name,
                        IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                    };
                    model.Add(addRoleModel);
                }
            }

            return View(model);
        }

        [HttpPost("{id}"),ActionName("AddRole")]
        public async Task<IActionResult> AddRoleAsync(string id, [FromForm] List<AddRoleModel> model)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            for(int i = 0; i <  model.Count; i++){
            Console.WriteLine(model[i].IsSelected);
            Console.WriteLine(model[i].roleName);
            Console.WriteLine(model[i].roleId);


                bool check = await _userManager.IsInRoleAsync(user, model[i].roleName);
                if(model[i].IsSelected && !check){
                    await _userManager.AddToRoleAsync(user, model[i].roleName);
                }

                if(!model[i].IsSelected && check){
                    Console.WriteLine("cv");
                    await _userManager.RemoveFromRoleAsync(user, model[i].roleName);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Address,Birthday,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,Address,Birthday,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
