using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Exo.WebApi.Repositories
{

    // Responsável direta pela manipulação com o banco de dados.
    public class ProjetoRepository
    {

        private readonly ExoContext _context;

        // Construtor
        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }

        // Lista todos os cadastros
        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }


        // Realiza cadastro
        public void Cadastrar(Projeto projeto)
        {

            _context.Projetos.Add(projeto);
            _context.SaveChanges();
        }


        // Busca um registro
        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }


        // Atualiza um registro
        public void Atualizar(int id, Projeto projeto)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);
            if (projetoBuscado != null)
            {
                projetoBuscado.NomeDoProjeto = projeto.NomeDoProjeto;
                projetoBuscado.Area = projeto.Area;
                projetoBuscado.Status = projeto.Status;
            }
            _context.Projetos.Update(projetoBuscado);
            _context.SaveChanges();
        }


        // Exclui registro
        public void Deletar(int id)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);
            _context.Projetos.Remove(projetoBuscado);
            _context.SaveChanges();
        }

    }
}