using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;

namespace NaPegada.Web.Areas.User.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserBUS _userBUS;

        public UserController()
        {
            _userBUS = new UserBUS();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                _userBUS.Register(userVM.User);
            }

            return View();
        }
    }
}
