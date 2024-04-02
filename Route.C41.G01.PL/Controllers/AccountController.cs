using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.ViewModels.User;

namespace Route.C41.G01.PL.Controllers
{
    public class AccountController : Controller
    {

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            
        }

        #region Sign Up (Register)

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Manual Mapping

                var user = new ApplicationUser()
                {
                    FName=model.FirstName,
                    LName=model.LastName,
                    UserName=model.UserName,
                    Email=model.Email,
                    IsAgree=model.IsAgree
                };

            }
            return View(model);
        }

        #endregion
    }
}
