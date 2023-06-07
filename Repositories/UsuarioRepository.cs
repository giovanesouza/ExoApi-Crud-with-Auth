using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Exo.WebApi.Repositories
{

    // Responsável direta pela manipulação com o banco de dados.
    public class UsuarioRepository
    {

        private readonly ExoContext _context;

        // Construtor
        public UsuarioRepository(ExoContext context)
        {
            _context = context;
        }


        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email ==
            email && u.Senha == senha);
        }



        // Lista todos os cadastros
        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }


        // Realiza cadastro
        public void Cadastrar(Usuario usuario)
        {

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }


        // Busca um registro
        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }


        // Atualiza um registro
        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
            }
            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }


        // Exclui registro
        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(usuarioBuscado);
            _context.SaveChanges();
        }

    }
}