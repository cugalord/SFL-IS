using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sfl.Data;
using sfl.Models;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace sfl.Controllers_Api
{
    [Route("api/v1/Login")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly CompanyContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginApiController(CompanyContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: api/LoginApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            // For testing purposes only
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            return await _context.Jobs.ToListAsync();
        }

        // POST: api/LoginApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JsonContent>> PostLogin([FromBody] ApiLoginModel user)
        {
            var ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            String username = user.Username;
            String password = user.Password;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Staff staff = _context.Staff.Where(s => s.Username == username).FirstOrDefault();
                return AcceptedAtAction("PostLogin", new { id = user.Username }, "SecretKey");
                //return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                //return JsonContent("User is locked out.");
                //return JsonContent.Create("User is locket out.");
                return StatusCode(403, "User is locked out.");
            }
            else
            {
                //return JsonContent.Create("Invalid login attempt.");
                return StatusCode(403, "User is locked out.");
            }
        }
    }
}
