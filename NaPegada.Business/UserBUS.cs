using NaPegada.Model;
using NaPegada.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Business
{
    public class UserBUS
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
    }
}
