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
                return RedirectToAction("LogIn");
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
                return View("Home", new UserViewModel { User = _userBUS.GetByMail(userVM.User.Mail) });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UserViewModel userVM, string id)
        {
            if (ModelState.IsValid)
            {
                _userBUS.Update(userVM.User, id);
                return View("Home", new UserViewModel { User = _userBUS.GetById(id) });
            }
            return View("MyProfile", new { id = id });
        }

        #endregion

        #region [VIEWS]

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

        [HttpGet]
        public ViewResult MyProfile(string id)
        {
            return View(new UserViewModel { User = _userBUS.GetById(id) });
        }

        #endregion

    }
}
