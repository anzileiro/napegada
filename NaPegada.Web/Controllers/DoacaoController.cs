using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Model.DTO;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Web.Models.Doacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
                var userBus = new UsuarioBUS();
                var doacao = await userBus.ObterDoacao(id);
                model = new DetalhesViewModel(doacao);
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
            var userBus = new UsuarioBUS();
            var dto = ObterDTO(model);
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

        private RegistroDoacaoDTO ObterDTO(DetalhesViewModel model)
        {
            var dto = new RegistroDoacaoDTO();
            var user = ObterUsuarioDaSecao();

            dto.Doacao = model.ConverterParaDoacao();
            dto.IdUsuario = user.Id;

            return dto;
        }

        [HttpGet]
        public async Task<PartialViewResult> Exclusao(string id)
        {
            var userBus = new UsuarioBUS();
            var doacao = await userBus.ObterDoacao(id);

            return PartialView("_DeletarDoacao", new ExclusaoViewModel(doacao));
        }

        [HttpPost]
        public async Task<ActionResult> Excluir(string id)
        {
            var userBus = new UsuarioBUS();

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