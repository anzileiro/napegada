using NaPegada.Model;
using NaPegada.Repository;
using System;

namespace NaPegada.Business
{
    public class UserBUS : Utility
    {
        private readonly UserREP _userREP;

        public UserBUS()
        {
            _userREP = new UserREP();
        }

        public void Register(UserMOD userMOD)
        {
            SetRoles(userMOD);
            userMOD.DateOfRegistration = DateTime.Now;
            _userREP.Register(userMOD);
        }

        public bool IsUser(UserMOD userMOD)
        {
            return _userREP.IsUser(userMOD);
        }

        public UserMOD GetByMail(string mail)
        {
            return _userREP.GetByMail(mail);
        }

        public UserMOD GetById(string id)
        {
            return _userREP.GetById(ConvertToId(id));
        }

        public void Update(UserMOD userMOD, string id)
        {
            userMOD.NameFile = VerifyAndSaveFile(userMOD.Upload.File, @"~/Content/upload/user");
            _userREP.Update(userMOD, ConvertToId(id));
        }

        private void SetRoles(UserMOD userMOD, int role = 0)
        {
            if (role > 0 && role <= 2)
            {
                userMOD.Role = role == 1 ? Roles.User : Roles.Admin;
            }
            else
            {
                userMOD.Role = Roles.User;
            }
        }

    }
}
