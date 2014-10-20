using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Tests.Stubs
{
    public class UsuarioREPStub : IUsuarioREP
    {
        private IList<UsuarioMOD> _usuarios = new List<UsuarioMOD>();

        #region site

        public async Task<IEnumerable<DoacaoMOD>> ObterTodasDoacoesExcetoUsuarioLogado(ObjectId idUsuarioLogado)
        {
            return await Task.Run(() => _usuarios.Where(_ => _.Id != idUsuarioLogado)
                                                 .SelectMany(_ => _.Doacoes)); 
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterTodasDoacoes()
        {
            return await Task.Run(() => _usuarios.SelectMany(_ => _.Doacoes));
        }

        #endregion site


        #region usuario

        #region perfil

        public async Task Registrar(UsuarioMOD usuario)
        {
            await Task.Run(() =>
            {
                _usuarios.Add(usuario);
            });
        }


        public Task<UsuarioMOD> EhUsuario(UsuarioMOD usuario)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioMOD> ObterPorId(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioMOD> ObterPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(UsuarioMOD usuario)
        {
            throw new NotImplementedException();
        }

        #endregion perfil

        #region doacao
        public async Task<DoacaoMOD> ObterDoacao(ObjectId id)
        {
            return await Task.Run(() => _usuarios.SelectMany(_ => _.Doacoes).FirstOrDefault(_ => _.Id == id));
        }

        public async Task RegistrarDoacao(RegistroDoacaoDTO dto)
        {
            await Task.Run(() => _usuarios.FirstOrDefault(_ => _.Id == dto.IdUsuario).AdicionarDoacao(dto.Doacao));
        }

        public async Task AtualizarDoacao(RegistroDoacaoDTO dto)
        {
            await Task.Run(() =>
            {
                var doacao = _usuarios.Where(_ => _.Id == dto.IdUsuario)
                                      .SelectMany(_ => _.Doacoes)
                                      .FirstOrDefault(_ => _.Id == dto.Doacao.Id);
                doacao.Caracteristicas = dto.Doacao.Caracteristicas;
                doacao.EhCastrado = dto.Doacao.EhCastrado;
                doacao.EhVacinado = dto.Doacao.EhVacinado;
                doacao.EspecieAnimal = dto.Doacao.EspecieAnimal;
                doacao.IdadeAnimal = dto.Doacao.IdadeAnimal;
                doacao.NomeAnimal = dto.Doacao.NomeAnimal;
                doacao.PorteAnimal = dto.Doacao.PorteAnimal;
                doacao.RacaAnimal = dto.Doacao.RacaAnimal;
                doacao.TomouVermifugo = dto.Doacao.TomouVermifugo;
            });
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId)
        {
            return await Task.Run(() => _usuarios.SelectMany(_ => _.Doacoes).ToList());
        }

        public async Task ExcluirDoacao(ExclusaoDoacaoDTO dto)
        {
            await Task.Run(() =>
            {
                var usuario = _usuarios.Single(_ => _.Id == dto.IdUsuario);
                var doacao = usuario.Doacoes.Single(_ => _.Id == dto.IdDoacao);

                usuario.ExcluirDoacao(doacao);
            });
        }

        public async Task<MensagemPrivadaDTO> ObterMensagemPrivadaDTO(AdocaoDTO dto)
        {
            return await Task.Run(() =>
            {
                var retorno = new MensagemPrivadaDTO
                {
                    Remetente = new MensageiroMOD
                    {
                        IdUsuario = dto.Adotante.Id
                    },
                    Doacao = new MensagemPrivadaDoacaoMOD
                    {
                        IdDoacao = dto.IdDoacao
                    }
                };

                return retorno;
            });            
        }

        #endregion doacao

        #region interesse
        public Task CadastrarInteresse(ObjectId userId, InteresseMOD interesse)
        {
            throw new NotImplementedException();
        }



        public Task<InteresseMOD> ObterInteresse(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task RegistrarInteresse(Model.DTO.Interesse.RegistroInteresseDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarInteresse(Model.DTO.Interesse.RegistroInteresseDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InteresseMOD>> ObterInteresses(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirInteresse(Model.DTO.Interesse.ExclusaoInteresseDTO dto)
        {
            throw new NotImplementedException();
        }
        #endregion interesse

        #endregion usuario            
    }
}
