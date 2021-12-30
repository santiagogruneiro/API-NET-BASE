using ApiParcial.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParcial.Repositorios
{
    public class ClientesRepositorio<TContext> : RepositorioBase<Cliente,TContext> where TContext : DbContext
    {
        public readonly TContext _context;
        public ClientesRepositorio(TContext context) : base(context) { _context = context; }




    }
}
