using NaPegada.DataAccess;
using NaPegada.Model;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace NaPegada.Repository
{
    public class UserREP
    {
        private readonly Connection<UserMOD> _conn;

        public UserREP()
        {
            _conn = new Connection<UserMOD>();
        }

        public void Register(UserMOD userMOD)
        {
            _conn.Connect("mongodb://localhost", "napegada", "user")
                 .Insert(userMOD);
        }

        public bool IsUser(UserMOD userMOD)
        {

            return _conn.Connect("mongodb://localhost", "napegada", "user")
                        .AsQueryable<UserMOD>()
                        .Any(u => u.Mail.Equals(userMOD.Mail) && u.Password.Equals(userMOD.Password));
        }

        public void Update(UserMOD userMOD)
        {
            _conn.Connect("mongodb://localhost", "napegada", "user")
                 .Save(userMOD);
        }

        public UserMOD GetByMail(string mail)
        {
            var queryByMail = Query.EQ("Mail", mail);
            var x = _conn.Connect("mongodb://localhost", "napegada", "user").FindOne(queryByMail);

            return x;
        }
    }
}
