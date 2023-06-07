using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{

    /*  Controlador da API. Ela trabalha com a classe ProjetoRepository.cs e com as operações de manipulação do banco de dados. 
    
    Route("api/controller") => Leva em consideração o nome do controller.
    Ex.: ProjetosController -> URL: api/projetos
    */

    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {

        // Alterado _projeto
        private readonly ProjetoRepository _projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        // Método responsável por listar todos os cadastros
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_projetoRepository.Listar());
        }


        // Realiza cadastro
        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            _projetoRepository.Cadastrar(projeto);
            return StatusCode(201);
        }


        // Busca um cadastro
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Projeto projeto = _projetoRepository.BuscarPorId(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return Ok(projeto);
        }


        // Atualiza registro
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            _projetoRepository.Atualizar(id, projeto);
            return StatusCode(204);
        }


        // Exclui registro
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _projetoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}


