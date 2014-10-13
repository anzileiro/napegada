using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Model.DTO.Interesse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Repository.Interfaces
{
    public interface IUsuarioREP
    {
        Task Registrar(UsuarioMOD usuarioMOD);

        Task<UsuarioMOD> EhUsuario(UsuarioMOD userMOD);

        Task Atualizar(UsuarioMOD userMOD);


        Task<UsuarioMOD> ObterPorEmail(string email);

        Task<UsuarioMOD> ObterPorId(ObjectId id);

        #region Doacao

        Task<DoacaoMOD> ObterDoacao(ObjectId id);

        Task RegistrarDoacao(RegistroDoacaoDTO dto);

        Task AtualizarDoacao(RegistroDoacaoDTO dto);

        Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId);

        Task ExcluirDoacao(ExclusaoDoacaoDTO dto);

        #endregion Doacao

        #region Interesse

        Task<InteresseMOD> ObterInteresse(ObjectId id);

        Task RegistrarInteresse(RegistroInteresseDTO dto);

        Task AtualizarInteresse(RegistroInteresseDTO dto);

        Task<IEnumerable<InteresseMOD>> ObterInteresses(ObjectId userId);

        Task ExcluirInteresse(ExclusaoInteresseDTO dto);

        #endregion Interesse
    }
}
