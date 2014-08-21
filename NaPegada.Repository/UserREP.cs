using NaPegada.DataAccess;
using NaPegada.Model;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace NaPegada.Repository
{
    public class UserREP
    {
        private Connection<UserMOD> _conn;

        public void Register(UserMOD userMOD)
        {
            using (_conn = new Connection<UserMOD>())
            {
                _conn.Connect("mongodb://localhost", "napegada", "user")
                     .Insert(userMOD);
            }
        }

        public bool IsUser(UserMOD userMOD)
        {
            using (_conn = new Connection<UserMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "user")
                        .AsQueryable<UserMOD>()
                        .Any(u => u.Mail.Equals(userMOD.Mail) && u.Password.Equals(userMOD.Password));
            }
        }

        public void Update(UserMOD userMOD)
        {
            using (_conn = new Connection<UserMOD>())
            {
                _conn.Connect("mongodb://localhost", "napegada", "user")
                     .Update(Query<UserMOD>.EQ(u => u.Id, userMOD.Id), Update<UserMOD>.Set(u => u.Name, userMOD.Name)
                                                                                      .Set(u => u.Password, userMOD.Password));
            }
        }

        public UserMOD GetByMail(string mail)
        {
            using (_conn = new Connection<UserMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "user")
                            .FindOne(Query.EQ("Mail", mail));
            }
        }

        public UserMOD GetById(ObjectId id)
        {
            using (_conn = new Connection<UserMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "user")
                            .FindOne(Query.EQ("_id", id));
            }
        }
    }
}
