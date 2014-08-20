using NaPegada.DataAccess;
using NaPegada.Model;

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
            _conn.Connect("mongodb://localhost", "napegada", "user");
            _conn.GetCollection().Insert(userMOD);
        }
    }
}
