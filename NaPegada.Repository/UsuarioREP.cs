using NaPegada.DataAccess;
using NaPegada.Model;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Threading.Tasks;
using NaPegada.Model.DTO;
using System.Collections;
using System.Collections.Generic;
using NaPegada.Repository.Interfaces;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Model.DTO.Interesse;

namespace NaPegada.Repository
{
    public class UsuarioREP : IUsuarioREP
    {
        private Conexao<UsuarioMOD> _conn;

        public async Task Registrar(UsuarioMOD usuarioMOD)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").Insert(usuarioMOD));
            }
        }

        public async Task<UsuarioMOD> EhUsuario(UsuarioMOD userMOD)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable<UsuarioMOD>().FirstOrDefault(u => u.Email.Equals(userMOD.Email) && u.Senha.Equals(userMOD.Senha)));
            }
        }

        public async Task Atualizar(UsuarioMOD userMOD)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
               await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                     .Update(Query<UsuarioMOD>.EQ(u => u.Id, userMOD.Id), Update<UsuarioMOD>
                                              .Set(u => u.NomeFotoPerfil, userMOD.NomeFotoPerfil)
                                              .Set(u => u.Senha, userMOD.Senha)
                                              .Set(u => u.Nome, userMOD.Nome)
                                              .Set(u => u.Email, userMOD.Email)
                                              .Set(u => u.Endereco, new EnderecoMOD
                                              {
                                                  Cep = userMOD.Endereco.Cep,
                                                  Bairro = userMOD.Endereco.Bairro,
                                                  Localidade = userMOD.Endereco.Localidade,
                                                  Logradouro = userMOD.Endereco.Logradouro,
                                                  Numero = userMOD.Endereco.Numero,
                                                  Uf = userMOD.Endereco.Uf,
                                                  Complemento = userMOD.Endereco.Complemento
                                              })));
            }
        }


        public async Task<UsuarioMOD> ObterPorEmail(string email)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").FindOne(Query.EQ("Email", email)));
            }
        }

        public async Task<UsuarioMOD> ObterPorId(ObjectId id)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").FindOne(Query.EQ("_id", id)));
            }
        }

        #region Doacao

        public async Task<DoacaoMOD> ObterDoacao(ObjectId id)
        {
            using(_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => {
                                                return (from usuario in _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()

                                                        select usuario.Doacoes.FirstOrDefault(_ => _.Id == id)).SingleOrDefault();
                                            });
            }
        }

        public async Task RegistrarDoacao(RegistroDoacaoDTO dto)
        {
            using(_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                                                 .Update(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario), 
                                                         Update<UsuarioMOD>.Push<DoacaoMOD>(_ => _.Doacoes, dto.Doacao)));
            }
        }

        public async Task AtualizarDoacao(RegistroDoacaoDTO dto)
        {
            using(_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() =>
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Doacoes._id", dto.Doacao.Id));
                    var update = Update.Set("Doacoes.$.NomeAnimal", dto.Doacao.NomeAnimal)
                                        .Set("Doacoes.$.PorteAnimal", dto.Doacao.PorteAnimal)
                                        .Set("Doacoes.$.TomouVermifugo", dto.Doacao.TomouVermifugo)
                                        .Set("Doacoes.$.EhCastrado", dto.Doacao.EhCastrado)
                                        .Set("Doacoes.$.EhVacinado", dto.Doacao.EhVacinado)
                                        .Set("Doacoes.$.EspecieAnimal", dto.Doacao.EspecieAnimal);

                    if (dto.Doacao.IdadeAnimal != null)
                        update.Set("Doacoes.$.IdadeAnimal", dto.Doacao.IdadeAnimal.ToBsonDocument<AnimalIdadeMOD>());

                    if(!string.IsNullOrWhiteSpace(dto.Doacao.RacaAnimal))
                        update.Set("Doacoes.$.RacaAnimal", dto.Doacao.RacaAnimal);

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
            }
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId)
        {
            using(_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                                                 .Where(_ => _.Id == userId)
                                                 .Select(_ => _.Doacoes)
                                                 .FirstOrDefault());
            }
        }

        public async Task ExcluirDoacao(ExclusaoDoacaoDTO dto)
        {
            using(_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() => 
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Doacoes._id", dto.IdDoacao));
                    var update = Update.PopFirst("Doacoes");

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);                    
                });
            }
        }

        #endregion Doacao

        #region Interesse

        public async Task<InteresseMOD> ObterInteresse(ObjectId id)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() =>
                {
                    return (from usuario in _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()

                            select usuario.Interesses.FirstOrDefault(_ => _.Id == id)).SingleOrDefault();
                });
            }
        }

        public async Task RegistrarInteresse(RegistroInteresseDTO dto)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                                                 .Update(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                                         Update<UsuarioMOD>.Push<InteresseMOD>(_ => _.Interesses, dto.Interesse)));
            }
        }

        public async Task AtualizarInteresse(RegistroInteresseDTO dto)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() =>
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Interesses._id", dto.Interesse.Id));
                    var update = Update.Set("Interesses.$.Raca", dto.Interesse.Raca)
                                        .Set("Interesses.$.IdadeMinimaEmAnos", dto.Interesse.IdadeMinimaEmAnos)
                                        .Set("Interesses.$.IdadeMaximaEmAnos", dto.Interesse.IdadeMaximaEmAnos)
                                        .Set("Interesses.$.Porte", dto.Interesse.Porte.ToBson())
                                        .Set("Interesses.$.TomouVermifugo", dto.Interesse.TomouVermifugo)
                                        .Set("Interesses.$.EhCastrado", dto.Interesse.EhCastrado)
                                        .Set("Interesses.$.EhVacinado", dto.Interesse.EhVacinado)
                                        .Set("Interesses.$.Especie", dto.Interesse.Especie);

                    
                    if (!string.IsNullOrWhiteSpace(dto.Interesse.Raca))
                        update.Set("Interesses.$.Raca", dto.Interesse.Raca);

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
            }
        }

        public async Task<IEnumerable<InteresseMOD>> ObterInteresses(ObjectId userId)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                                                 .Where(_ => _.Id == userId)
                                                 .Select(_ => _.Interesses)
                                                 .FirstOrDefault());
            }
        }

        public async Task ExcluirInteresse(ExclusaoInteresseDTO dto)
        {
            using (_conn = new Conexao<UsuarioMOD>())
            {
                await Task.Run(() =>
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Interesses._id", dto.IdInteresse));
                    var update = Update.PopFirst("Interesses");

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
            }
        }

        #endregion Interesse
    }
}
