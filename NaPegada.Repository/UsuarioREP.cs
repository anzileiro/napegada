using NaPegada.DataAccess;
using NaPegada.Model;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace NaPegada.Repository
{
    public class UsuarioREP
    {
        private Connection<UsuarioMOD> _conn;

        public void Registrar(UsuarioMOD usuarioMOD)
        {
            using (_conn = new Connection<UsuarioMOD>())
            {
                _conn.Connect("mongodb://localhost", "napegada", "usuario")
                     .Insert(usuarioMOD);
            }
        }

        public bool EhUsuario(UsuarioMOD userMOD)
        {
            using (_conn = new Connection<UsuarioMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "usuario")
                        .AsQueryable<UsuarioMOD>()
                        .Any(u => u.Email.Equals(userMOD.Email) && u.Senha.Equals(userMOD.Senha));
            }
        }

        public void Atualizar(UsuarioMOD userMOD, ObjectId id)
        {
            using (_conn = new Connection<UsuarioMOD>())
            {
                _conn.Connect("mongodb://localhost", "napegada", "usuario")
                     .Update(Query<UsuarioMOD>.EQ(u => u.Id, id), Update<UsuarioMOD>
                                              .Set(u => u.NomeFotoPerfil, userMOD.NomeFotoPerfil)
                                              .Set(u => u.Senha, userMOD.Senha)
                                              .Set(u => u.Nome, userMOD.Nome)
                                              .Set(u => u.Endereco, new EnderecoMOD
                                              {
                                                  Cep = userMOD.Endereco.Cep,
                                                  Logradouro = userMOD.Endereco.Logradouro,
                                                  Numero = userMOD.Endereco.Numero,
                                                  Bairro = userMOD.Endereco.Bairro,
                                                  Cidade = userMOD.Endereco.Cidade,
                                                  Estado = userMOD.Endereco.Estado
                                              }));
            }
        }

        public UsuarioMOD ObterPorEmail(string email)
        {
            using (_conn = new Connection<UsuarioMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "usuario")
                            .FindOne(Query.EQ("Email", email));
            }
        }

        public UsuarioMOD ObterPorId(ObjectId id)
        {
            using (_conn = new Connection<UsuarioMOD>())
            {
                return _conn.Connect("mongodb://localhost", "napegada", "usuario")
                            .FindOne(Query.EQ("_id", id));
            }
        }


    }
}
