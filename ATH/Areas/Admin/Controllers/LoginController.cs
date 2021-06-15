using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ATH.Models;
using ATH.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace ATH.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public LoginController()
        {

        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Admin/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid email or password";
                //return View("Index", model);
                return RedirectToAction("Index");
            }

            await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (SignInStatus.Success == 0)
            {

                ApplicationUser applicationUser = UserManager.Find(model.Email, model.Password);

                if (applicationUser != null)
                {
                    IEnumerable<string> userRoles = UserManager.GetRoles(applicationUser.Id);
                    if (userRoles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }

                ModelState.AddModelError("", "Invalid User Name or Password");

                //var user = await UserManager.FindAsync(model.Email, model.Password);
                //var roles = await UserManager.GetRolesAsync(user.Id);

                //bool isAuthenticated = User.Identity.IsAuthenticated;
                //bool isAdmin = User.IsInRole("Admin");


                //if (roles.Contains("Admin"))
                //{
                //    return RedirectToAction("Index", "Dashboard");
                //}

                //bool temp = User.IsInRole("Admin");
                //if (User.Identity.IsAuthenticated)
                //{
                //    return RedirectToAction("Index", "Dashboard");
                //}
            }

            //return View("Index", model);
            return RedirectToAction("Index");
        }


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Dashboard");

        }

    }
}