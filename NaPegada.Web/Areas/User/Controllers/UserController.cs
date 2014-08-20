using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;

namespace NaPegada.Web.Areas.User.Controllers
{
    [Authorize]
    public class UserController : AuthController
    {
        private readonly UserBUS _userBUS;

        public UserController()
        {
            _userBUS = new UserBUS();
        }

        #region [ACTIONS]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                _userBUS.Register(userVM.User);

                return RedirectToAction("LogIn", new
                {
                    userVM = userVM
                });
            }

            return RedirectToAction("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogIn(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                SignIn(userVM);
                return RedirectToAction("Home", new { mail = userVM.User.Mail });
            }

            return RedirectToAction("LogIn");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            SignOut();
            return RedirectToAction("LogIn");
        }
        #endregion

        #region [VIEWS]
        [HttpGet]
        public ViewResult Home(string mail)
        {
            return View(new UserViewModel { User = _userBUS.GetByMail(mail) });
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

        #endregion

    }
}
