using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Model.DTO;
using NaPegada.Repository;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Web.Models.Doacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NaPegada.Model;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    public class DoacaoController : BaseAsyncController
    {        
        [HttpGet]
        public async Task<PartialViewResult> Detalhes(string id = null)
        {
            var model = default(DetalhesViewModel);

            if(!string.IsNullOrWhiteSpace(id))
            {
                var userBus = new UsuarioBUS(new UsuarioREP());
                var racaBus = new RacaBUS();                
                var doacao = await userBus.ObterDoacao(id);
                var racas = await racaBus.BuscarPorEspecie(doacao.EspecieAnimal);
                model = new DetalhesViewModel(doacao, racas);
            }
            else
            {
                model = new DetalhesViewModel();
            }

            return PartialView("_Doacao", model);
        }        

        [HttpPost]
        public async Task<ActionResult> Detalhes(DetalhesViewModel model)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());
            var userId = ObterUsuarioDaSecao().Id;
            var dto = await model.ConverterParaRegistroDoacaoDTO(userId);
            var ehCadastro = string.IsNullOrWhiteSpace(model.Id);

            if(ehCadastro)
            {                
                await userBus.RegistrarDoacao(dto);
                TempData["sucesso"] = "Doação cadastrada com sucesso";
            }
            else
            {
                await userBus.AtualizarDoacao(dto);
                TempData["sucesso"] = "Doação atualizada com sucesso";
            }            

            return RedirectToAction("MinhasDoacoes", "Usuario");
        }

        [HttpGet]
        public async Task<PartialViewResult> Exclusao(string id)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());
            var doacao = await userBus.ObterDoacao(id);

            return PartialView("_DeletarDoacao", new ExclusaoViewModel(doacao));
        }

        [HttpPost]
        public async Task<ActionResult> Excluir(string id)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());

            await userBus.ExcluirDoacao(ObterDTO(id));
            TempData["sucesso"] = "Doação excluída com sucesso";

            return RedirectToAction("MinhasDoacoes", "Usuario");
        }

        private ExclusaoDoacaoDTO ObterDTO(string id)
        {
            var dto = new ExclusaoDoacaoDTO();

            dto.IdDoacao = ObjectId.Parse(id);
            dto.IdUsuario = ObterUsuarioDaSecao().Id;

            return dto;
        }        
	}
}