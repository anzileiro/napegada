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
        private Conexao<UsuarioMOD> _conn = new Conexao<UsuarioMOD>();

        #region site

        public async Task<IEnumerable<DoacaoMOD>> ObterTodasDoacoesExcetoUsuarioLogado(ObjectId idUsuarioLogado)
        {
            return await Task.Run(() =>
                {
                    var lista = _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                        .FindAs<UsuarioMOD>(Query<UsuarioMOD>.NE(_ => _.Id, idUsuarioLogado))
                        .SetFields(Fields<UsuarioMOD>.Include(_ => _.Doacoes)).ToList();

                    return lista.SelectMany(_ => _.Doacoes);
                });
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterTodasDoacoes()
        {
            return await Task.Run(() =>
            {
                var lista = _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                    .FindAllAs<UsuarioMOD>()
                    .SetFields(Fields<UsuarioMOD>.Include(_ => _.Doacoes)).ToList();

                return lista.SelectMany(_ => _.Doacoes);
            });
        }

        #endregion site


        #region usuario

        #region perfil
        public async Task Registrar(UsuarioMOD usuarioMOD)
        {
            await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").Insert(usuarioMOD));
        }

        public async Task<UsuarioMOD> EhUsuario(UsuarioMOD userMOD)
        {
            return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable<UsuarioMOD>().FirstOrDefault(u => u.Email.Equals(userMOD.Email) && u.Senha.Equals(userMOD.Senha)));
        }

        public async Task Atualizar(UsuarioMOD userMOD)
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


        public async Task<UsuarioMOD> ObterPorEmail(string email)
        {
            return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").FindOne(Query.EQ("Email", email)));
        }

        public async Task<UsuarioMOD> ObterPorId(ObjectId id)
        {
            return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").FindOne(Query.EQ("_id", id)));
        }

        #endregion perfil

        #region Doacao

        public async Task<DoacaoMOD> ObterDoacao(ObjectId id)
        {
            return await Task.Run(() => {
                                                return (from usuario in _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                                                        where usuario.Doacoes.Any(_ => _.Id == id)
                                                        select usuario.Doacoes.FirstOrDefault(_ => _.Id == id)).SingleOrDefault();
                                            });
        }

        public async Task RegistrarDoacao(RegistroDoacaoDTO dto)
        {
            await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                                                 .Update(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario), 
                                                         Update<UsuarioMOD>.Push<DoacaoMOD>(_ => _.Doacoes, dto.Doacao)));
        }

        public async Task AtualizarDoacao(RegistroDoacaoDTO dto)
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
                                        .Set("Doacoes.$.EspecieAnimal", dto.Doacao.EspecieAnimal)
                                        .Set("Doacoes.$.Fotos", new BsonArray(dto.Doacao.Fotos));

                    if (dto.Doacao.IdadeAnimal != null)
                        update.Set("Doacoes.$.IdadeAnimal", dto.Doacao.IdadeAnimal.ToBsonDocument<AnimalIdadeMOD>());

                    if(!string.IsNullOrWhiteSpace(dto.Doacao.RacaAnimal))
                        update.Set("Doacoes.$.RacaAnimal", dto.Doacao.RacaAnimal);

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId)
        {
            return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                                                 .Where(_ => _.Id == userId)
                                                 .Select(_ => _.Doacoes)
                                                 .FirstOrDefault());
        }

        public async Task ExcluirDoacao(ExclusaoDoacaoDTO dto)
        {
            await Task.Run(() => 
                {
                    var query = Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario);
                    var update = Update.Pull("Doacoes", Query.EQ("_id", dto.IdDoacao));

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);                    
                });
        }

        #endregion Doacao

        #region Interesse

        public async Task<InteresseMOD> ObterInteresse(ObjectId id)
        {
            return await Task.Run(() =>
                {
                    return (from usuario in _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                            where usuario.Interesses.Any(_ => _.Id == id)
                            select usuario.Interesses.FirstOrDefault(_ => _.Id == id)).SingleOrDefault();
                });
        }

        public async Task RegistrarInteresse(RegistroInteresseDTO dto)
        {
            await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario")
                                                 .Update(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                                         Update<UsuarioMOD>.Push<InteresseMOD>(_ => _.Interesses, dto.Interesse)));
        }

        public async Task AtualizarInteresse(RegistroInteresseDTO dto)
        {
            await Task.Run(() =>
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Interesses._id", dto.Interesse.Id));

                    var portes = new BsonArray();
                    portes.AddRange(dto.Interesse.Porte);

                    var update = Update .Set("Interesses.$.IdadeMinimaEmAnos", dto.Interesse.IdadeMinimaEmAnos)
                                        .Set("Interesses.$.IdadeMaximaEmAnos", dto.Interesse.IdadeMaximaEmAnos)
                                        .Set("Interesses.$.Porte", portes)
                                        .Set("Interesses.$.TomouVermifugo", dto.Interesse.TomouVermifugo)
                                        .Set("Interesses.$.EhCastrado", dto.Interesse.EhCastrado)
                                        .Set("Interesses.$.EhVacinado", dto.Interesse.EhVacinado)
                                        .Set("Interesses.$.Especie", dto.Interesse.Especie);

                    
                    if (!string.IsNullOrWhiteSpace(dto.Interesse.Raca))
                        update.Set("Interesses.$.Raca", dto.Interesse.Raca);

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
        }

        public async Task<IEnumerable<InteresseMOD>> ObterInteresses(ObjectId userId)
        {
            return await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable()
                                                 .Where(_ => _.Id == userId)
                                                 .Select(_ => _.Interesses)
                                                 .FirstOrDefault());
        }

        public async Task ExcluirInteresse(ExclusaoInteresseDTO dto)
        {
            await Task.Run(() =>
                {
                    var query = Query.And(Query<UsuarioMOD>.EQ(_ => _.Id, dto.IdUsuario),
                                          Query.EQ("Interesses._id", dto.IdInteresse));
                    var update = Update.Pull("Interesses", Query.EQ("_id", dto.IdInteresse));

                    _conn.Conectar("mongodb://localhost", "napegada", "usuario").Update(query, update);
                });
        }

        #endregion Interesse

        #endregion usuario       
    }
}
