using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Areas.User.Controllers
{
    public abstract class AuthController : Controller
    {
        private readonly UserBUS _userBUS;

        public AuthController()
        {
            _userBUS = new UserBUS();
        }

        public void SignIn(UserViewModel userVM)
        {
            if (_userBUS.IsUser(userVM.User))
            {
                FormsAuthentication.Authenticate(userVM.User.Mail, userVM.User.Password);
                FormsAuthentication.SetAuthCookie(userVM.User.Mail, userVM.User.StayConnected);
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}
