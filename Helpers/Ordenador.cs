using System.Linq;
using System.Reflection;
using System.Text;
using System;
using System.Linq.Dynamic.Core;

namespace Clase4.Helpers
{
    public class Ordenador<T>
    {
            //nombre,
            //precio,
            //marca 
        public static IQueryable<T> Ordenar(IQueryable entidades,string CampoOrdenamiento){

            var parametros= CampoOrdenamiento.Trim().Split(',');
            var infopropiedades= typeof(T).GetProperties(BindingFlags.Public |BindingFlags.Instance);
            var BuilderOrden= new StringBuilder();

            foreach( var par in parametros){
                
                var propQuery=par.Trim().Split(" ");
                propQuery[0] = propQuery[0].Trim(); 
                var objprop= infopropiedades.FirstOrDefault(x=>x.Name.ToLower().Equals(propQuery[0].ToLower()));

      
                if (propQuery.Length > 1)
                    BuilderOrden.Append($"{objprop.Name} {propQuery[1]},");
                else
                    BuilderOrden.Append($"{objprop.Name}, ");
            }

            var consultaOrden=BuilderOrden.ToString().TrimEnd(',',' ');
            return (IQueryable<T>)entidades.OrderBy(consultaOrden);
            
        }
        
    }
}