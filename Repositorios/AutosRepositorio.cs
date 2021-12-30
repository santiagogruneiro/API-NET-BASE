using ApiParcial.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParcial.Repositorios
{
    public class AutosRepositorio<TContext> : RepositorioBase<Auto, TContext> where TContext : DbContext
    {
        public readonly TContext _context;
        public AutosRepositorio(TContext context) : base(context) { _context = context; }

        public override List<Auto> FindAll()
        {
            var lst = _context.Set<Auto>().Include(x=>x.Venta).ToList();
            return lst;
        }

        public List<Auto> FindVendidos()
        {
            var autos = FindAll();
            autos = autos.Where(e => e.Venta.Count > 0).ToList();
            if (autos.Count > 0)
            {
                return autos;
            }
            else
            {
                throw new NullReferenceException("No se encontraron autos con ventas");
            }
        }

        public List<Auto> FindDisponibles()
        {
            var autos = FindAll();
            autos = autos.Where(e => e.Venta.Count == 0).ToList();
            if (autos.Count > 0)
            {
                return autos;
            }
            else
            {
                throw new NullReferenceException("No se encontraron autos disponibles");
            }
        }
    }
}
