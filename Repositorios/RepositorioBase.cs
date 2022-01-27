using ApiParcial.Model;
using Clase4.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiParcial.Repositorios
{
    public class RepositorioBase<T, TContext> : IRepository<T> where TContext : DbContext where T : ModeloBase
    {
        private readonly TContext _context;
        public RepositorioBase(TContext context)
        {
            _context = context;
        }
        public virtual void Actualizar(T entidad)
        {
           
            this._context.Entry(entidad).State = EntityState.Modified;
            this._context.SaveChanges();
        }

        public virtual void Borrar(string id)
        {

            var entity = FindById(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
            }
           
        }

        public virtual void Crear(T entidad)
        {
            this._context.Set<T>().Add(entidad);
            this._context.SaveChanges();
        }

        public virtual List<T> FindAll()
        {

            return _context.Set<T>().ToList();
        }

        public virtual T FindById(string id)
        {
            T entity;
            bool result = id.All(Char.IsDigit);
            if (result)
            {
                int newId;
                newId = Convert.ToInt32(id);
                entity = _context.Set<T>().Find(newId);
            }
            else
                entity = _context.Set<T>().Find(id);

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        private  bool EntityExists(T entity)
        {
            T entidad =  _context.Set<T>().Where(e => e == entity).FirstOrDefault();
            return entidad == null ? false : true;
        }
       
    }
}
