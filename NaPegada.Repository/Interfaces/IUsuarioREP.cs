using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Repository.Interfaces
{
    public interface IUsuarioREP
    {
        Task<DoacaoMOD> ObterDoacao(ObjectId id);

        Task RegistrarDoacao(RegistroDoacaoDTO dto);

        Task AtualizarDoacao(RegistroDoacaoDTO dto);

        Task<IEnumerable<DoacaoMOD>> ObterDoacoes(ObjectId userId);

        Task ExcluirDoacao(ExclusaoDoacaoDTO dto);

        Task Registrar(UsuarioMOD usuario);

        Task CadastrarInteresse(ObjectId userId, InteresseMOD interesse);

        Task<UsuarioMOD> EhUsuario(UsuarioMOD usuario);

        Task<UsuarioMOD> ObterPorId(ObjectId id);

        Task<UsuarioMOD> ObterPorEmail(string email);

        Task Atualizar(UsuarioMOD usuario);
    }
}
