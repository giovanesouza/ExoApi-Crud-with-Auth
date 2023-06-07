using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exo.WebApi.Controllers
{


    /*  Controlador da API. Ela trabalha com a classe UsuarioRepository.cs e com as operações de manipulação do banco de dados. 

Route("api/controller") => Leva em consideração o nome do controller.
Ex.: UsuariosController -> URL: api/usuarios
*/


    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // get --> /api/usuarios
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }

        // post --> /api/usuarios
        // [HttpPost]
        // public IActionResult Cadastrar(Usuario usuario)
        // {
        //     _usuarioRepository.Cadastrar(usuario);
        //     return StatusCode(201);
        // }


        // Novo código POST para auxiliar o método de Login.
        public IActionResult Post(Usuario usuario)
        {

            /* Se não existir o usuário -> NotFound
                Se existir, cria o token
            */
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email,
            usuario.Senha);
            if (usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos!");
            }

            // Se o usuário for encontrado, segue a criação do token.
            // Define os dados que serão fornecidos no token - Payload
            var claims = new[]
            {

            // Armazena na claim o e mail usuário autenticado.
            new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

            // Armazena na claim o id do usuário autenticado.
            new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()) };

            // Define a chave de acesso ao token.
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

            // Define as credenciais do token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token.
            var token = new JwtSecurityToken(
                issuer: "exoapi.webapi", // Emissor do token.
                audience: "exoapi.webapi", // Destinatário do token.
                claims: claims, // Dados definidos acima.
                expires: DateTime.Now.AddMinutes(30), // Tempo de expiração.
                signingCredentials: creds // Credenciais do token
            );

            // Retorna ok com o token.
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );
        }
        // Fim do novo código POST para auxiliar o método de Login.


        // get --> /api/usuarios/{id}
        // Busca um cadastro
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [Authorize] // Apenas usuários autorizados podem ATUALIZAR registros de usuários
        // put --> /api/usuarios/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }


        [Authorize] // Apenas usuários autorizados podem EXCLUIR registros de usuários
        // delete --> /api/usuarios/{id}
        // Exclui registro
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}
