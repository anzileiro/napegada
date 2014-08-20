using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Update(UserMOD userMOD)
        {
            _userREP.Update(userMOD);
        }
    }
}
