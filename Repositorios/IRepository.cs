using System.Collections.Generic;

namespace ApiParcial.Repositorios
{
    public interface IRepository<T>
    {
        T FindById(string id);
        List<T> FindAll();
        void Crear(T entidad);

        void Actualizar(T entidad);

        void Borrar(string id);
    }
}