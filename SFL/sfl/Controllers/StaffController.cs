using sfl.Data;
using sfl.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;

namespace sfl.Controllers
{
    [Authorize(Roles = "Administrator, Warehouse manager")]
    public class StaffController : Controller
    {
        private readonly CompanyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string[] _navigationProperties;
        private readonly string[] _autogeneratedAttributes;

        public StaffController(CompanyContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _navigationProperties = new string[] { "Role", "Branch" };
            _autogeneratedAttributes = new string[] { "Username" };
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            int currentBranch;

            // If current user administrator, view all branches, if not, view only their branch.
            if (_context.UserRoles.Any(ur => ur.UserId == currentUser.Id && ur.RoleId == "1"))
            {
                currentBranch = -1;
                return View(await _context.Staff
                    .Include(s => s.Branch)
                    .Include(s => s.Role).ToListAsync());
            }
            else
            {
                currentBranch = _context.Staff.Select(s => new { s.Username, s.BranchID })
                    .Where(s => s.Username == currentUser.UserName)
                    .ToList()[0].BranchID;

                return View(await _context.Staff
                    .Include(s => s.Branch)
                    .Include(s => s.Role)
                    .Where(s => s.BranchID == currentBranch).ToListAsync());
            }
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var staff = await _context.Staff
                .Include(s => s.Branch)
                .Include(s => s.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name");
            ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Name,Surname,Password,ConfirmPassword,PhoneNumber,BranchID,Branch,RoleID,Role")] Staff staff)
        {
            var userName = staff.Name.ToLower() + "." + staff.Surname.ToLower() + "@sfl.si";

            if (Request.Form["Password"].ToString() != Request.Form["ConfirmPassword"].ToString())
            {
                ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name", staff.BranchID);
                ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name", staff.RoleID);
                return View(staff);
            }

            RemoveAutogeneratedAttributes();
            RemoveNavigationProperties();
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Name = staff.Name,
                    Surname = staff.Surname,
                    UserName = userName,
                    NormalizedUserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    NormalizedEmail = "XXXX@SFL.SI",
                    PhoneNumber = Request.Form["PhoneNumber"].ToString(),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, Request.Form["Password"].ToString());
                user.PasswordHash = hashed;
                _context.Users.Add(user);

                var role = _context.Roles
                    .Select(r => r)
                    .Where(r => r.Id == staff.RoleID.ToString()).First();

                var userRole = new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                };

                _context.UserRoles.Add(userRole);

                staff.Username = user.UserName;

                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name", staff.BranchID);
            ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name", staff.RoleID);
            return View(staff);
        }

        // GET: Staff/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name", staff.BranchID);
            ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name", staff.RoleID);
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Name,Surname,Password,ConfirmPassword,BranchID,RoleID")] Staff staff)
        {
            if (id != staff.Username)
            {
                return NotFound();
            }

            if (staff.Password != staff.ConfirmPassword)
            {
                ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name", staff.BranchID);
                ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name", staff.RoleID);
                return View(staff);
            }

            RemoveNavigationProperties();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Users
                        .Select(u => u)
                        .Where(u => u.UserName == staff.Username)
                        .First();

                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, staff.Password);
                    user.PasswordHash = hashed;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Username))
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
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "Name", staff.BranchID);
            ViewData["RoleID"] = new SelectList(_context.StaffRoles, "ID", "Name", staff.RoleID);
            return View(staff);
        }

        // GET: Staff/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Branch)
                .Include(s => s.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'CompanyContext.Staff'  is null.");
            }
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);

                var user = await _context.Users
                    .Select(u => u)
                    .Where(u => u.UserName == staff.Username).FirstAsync();
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
            return (_context.Staff?.Any(e => e.Username == id)).GetValueOrDefault();
        }

        private void RemoveNavigationProperties()
        {
            foreach (var key in _navigationProperties)
            {
                ModelState.Remove(key);
            }
        }

        private void RemoveAutogeneratedAttributes()
        {
            foreach (var key in _autogeneratedAttributes)
            {
                ModelState.Remove(key);
            }
        }
    }
}
