using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Model.DTO.Interesse;
using NaPegada.Repository;
using NaPegada.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace NaPegada.Business
{
    public class UsuarioBUS : AvisoSistema
    {
        private readonly IUsuarioREP _usuarioREP;
        private readonly Utilitaria _utilitaria;

        public UsuarioBUS(IUsuarioREP usuarioREP)
        {
            _usuarioREP = usuarioREP;
            _utilitaria = new Utilitaria();
        }

        #region site

        public async Task<IEnumerable<DoacaoMOD>> ObterTodasDoacoes()
        {
            return await _usuarioREP.ObterTodasDoacoes();
        }

        #endregion site



        #region usuario

        #region perfil


        public async Task Registrar(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha);
            await _usuarioREP.Registrar(usuarioMOD);
        }

        public async Task<UsuarioMOD> EhUsuario(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha);
            return await _usuarioREP.EhUsuario(usuarioMOD);
        }

        public async Task<UsuarioMOD> ObterPorEmail(string email)
        {
            return await _usuarioREP.ObterPorEmail(email);
        }

        public async Task<UsuarioMOD> ObterPorId(string id)
        {
            return await _usuarioREP.ObterPorId(ConverterParaObjectId(id));
        }

        public async Task Atualizar(UsuarioMOD usuarioMOD, HttpPostedFileBase arquivo)
        {
            if (arquivo != null)
            {
                usuarioMOD.NomeFotoPerfil = _utilitaria.VerificaEhSalvaArquivo(arquivo, @"~/Content/upload/usuario").Result;
            }
            else
            {
                usuarioMOD.NomeFotoPerfil = usuarioMOD.NomeFotoPerfil;
            }
            await _usuarioREP.Atualizar(usuarioMOD);
        }

        public ObjectId ConverterParaObjectId(string s)
        {
            return ObjectId.Parse(s);
        }

        #endregion perfil

        #region Doacao

        public async Task<DoacaoMOD> ObterDoacao(string id)
        {
            var mongoId = ConverterParaObjectId(id);

            return await _usuarioREP.ObterDoacao(mongoId);
        }

        public async Task RegistrarDoacao(RegistroDoacaoDTO dto)
        {
            await _usuarioREP.RegistrarDoacao(dto);
        }

        public async Task AtualizarDoacao(RegistroDoacaoDTO dto)
        {
            await _usuarioREP.AtualizarDoacao(dto);
        }

        public async Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId)
        {
            return await _usuarioREP.ObterDoacoes(userId);
        }

        public async Task ExcluirDoacao(ExclusaoDoacaoDTO dto)
        {
            await _usuarioREP.ExcluirDoacao(dto);
        }

        #endregion Doacao

        #region Interesse

        public async Task<InteresseMOD> ObterInteresse(string id)
        {
            var mongoId = ConverterParaObjectId(id);

            return await _usuarioREP.ObterInteresse(mongoId);
        }

        public async Task RegistrarInteresse(RegistroInteresseDTO dto)
        {
            await _usuarioREP.RegistrarInteresse(dto);
        }

        public async Task AtualizarInteresse(RegistroInteresseDTO dto)
        {
            await _usuarioREP.AtualizarInteresse(dto);
        }

        public async Task<IEnumerable<InteresseMOD>> ObterInteresses(ObjectId userId)
        {
            return await _usuarioREP.ObterInteresses(userId);
        }

        public async Task ExcluirInteresse(ExclusaoInteresseDTO dto)
        {
            await _usuarioREP.ExcluirInteresse(dto);
        }

        #endregion Interesse

        #endregion usuario
    }
}
