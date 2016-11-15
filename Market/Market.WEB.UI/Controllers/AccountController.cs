using Market.WEB.UI.Infrastructure.Abstract;
using Market.WEB.UI.Models;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;

namespace Market.WEB.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
              return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}