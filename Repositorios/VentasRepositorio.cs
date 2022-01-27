using ApiParcial.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParcial.Repositorios
{
    public class VentasRepositorio<TContext> : RepositorioBase<Venta, TContext> where TContext : DbContext
    {
        public readonly TContext _context;
        public VentasRepositorio(TContext context) : base(context) { _context = context; }

        public override List<Venta> FindAll()
        {
            return _context.Set<Venta>().Include(x => x.Cliente).Include(x=>x.PatenteNavigation).ToList();
        }
       
    }

}
