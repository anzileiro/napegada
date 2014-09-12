using NaPegada.DataAccess;
using NaPegada.Model;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Collections.Generic;

namespace NaPegada.Repository
{
    public class UsuarioREP
    {
        private Conexao<UsuarioMOD> _conn;

        public void Registrar(UsuarioMOD usuarioMOD)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                     .Insert(usuarioMOD);
            }
        }

        public bool EhUsuario(UsuarioMOD userMOD)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                        .AsQueryable<UsuarioMOD>()
                        .Any(u => u.Email.Equals(userMOD.Email) && u.Senha.Equals(userMOD.Senha));
            }
        }

        public void Atualizar(UsuarioMOD userMOD, ObjectId id)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                _conn.Conectar("mongodb://localhost", "napegada", "usuario")
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
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                            .FindOne(Query.EQ("Email", email));
            }
        }

        public UsuarioMOD ObterPorId(ObjectId id)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                            .FindOne(Query.EQ("_id", id));
            }
        }



        public IEnumerable<PesquisaMOD> Pesquisar(string dadosPesquisa)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                            .AsQueryable<UsuarioMOD>()
                            .Where(u => u.Nome.Contains(dadosPesquisa))
                            .ToList().ConvertAll(u => new PesquisaMOD 
                            {
                                Id = u.Id,
                                Titulo = u.Nome,
                                Descricao = u.Email
                            });
            }
        }
    }
}
