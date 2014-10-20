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
using NaPegada.Web.Models.Usuario;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    public class DoacaoController : BaseAsyncController
    {
        private readonly UsuarioBUS _usuarioBUS;
        private readonly MensagemPrivadaBUS _mensagemPrivadaBUS;
        private readonly RacaBUS _racaBUS;

        public DoacaoController()
        {
            var usuarioREP = new UsuarioREP();
            _usuarioBUS = new UsuarioBUS(usuarioREP);
            _mensagemPrivadaBUS = new MensagemPrivadaBUS();
            _racaBUS = new RacaBUS();
        }

        [HttpGet]
        public async Task<PartialViewResult> Detalhes(string id = null)
        {
            var model = default(DetalhesViewModel);

            if(!string.IsNullOrWhiteSpace(id))
            {               
                var doacao = await _usuarioBUS.ObterDoacao(id);
                var racas = await _racaBUS.BuscarPorEspecie(doacao.EspecieAnimal);
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
            var userId = ObterUsuarioDaSecao().Id;
            var dto = await model.ConverterParaRegistroDoacaoDTO(userId);
            var ehCadastro = string.IsNullOrWhiteSpace(model.Id);

            if(ehCadastro)
            {                
                await _usuarioBUS.RegistrarDoacao(dto);
                TempData["sucesso"] = "Doação cadastrada com sucesso";
            }
            else
            {
                await _usuarioBUS.AtualizarDoacao(dto);
                TempData["sucesso"] = "Doação atualizada com sucesso";
            }            

            return RedirectToAction("MinhasDoacoes", "Usuario");
        }

        [HttpGet]
        public async Task<PartialViewResult> Exclusao(string id)
        {
            var doacao = await _usuarioBUS.ObterDoacao(id);

            return PartialView("_DeletarDoacao", new ExclusaoViewModel(doacao));
        }

        [HttpPost]
        public async Task<ActionResult> Excluir(string id)
        {
            await _usuarioBUS.ExcluirDoacao(ObterDTO(id));
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
        
        [HttpPost]
        public async Task<JsonResult> Adocao(string idDoacao)
        {
            var json = default(dynamic);

            try
            {
                var dto = new AdocaoDTO(idDoacao, ObterUsuarioDaSecao());
                await _mensagemPrivadaBUS.EnviarMensagemAdocao(dto);

                json = new
                {
                    Mensagem = "Notificação enviada ao doador",
                    Sucesso = true
                };
            }
            catch(InvalidOperationException e)
            {
                json = new
                {
                    Mensagem = e.Message,
                    Sucesso = false
                };
            }
            catch(Exception)
            {
                json = new
                {
                    Mensagem = "Ocorreu um erro ao tentar enviar a notificação ao doador",
                    Sucesso = false
                };
            }

            return Json(json);
        }
	}
}