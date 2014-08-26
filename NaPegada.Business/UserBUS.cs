using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Repository;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Hosting;
using System.IO;

namespace NaPegada.Business
{
    public class UserBUS : Alert
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
            userMOD.NameFile = SaveFile(userMOD.Upload.File, @"~/Content/upload/user");
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

        private ObjectId ConvertToId(string s)
        {
            return ObjectId.Parse(s);
        }

        private string SaveFile(HttpPostedFileBase file, string path)
        {
            file.SaveAs(Path.Combine(HostingEnvironment.MapPath(path), file.FileName));

            return file.FileName;
        }
    }
}
