using System.Collections.Generic;
using System.Linq;
using System;
namespace Clase4.Helpers
{
    public class ListaPaginada<T>: List<T>
    {
        public static int PagActual {get;set;}
        public static int TotalPaginas{get;set;}
        public static int TamPag{get;set;}
        public static int TotalReg{get;set;}

        public static bool TieneAnt {
            get{
                return PagActual>1;
                }
        }
        public static bool TieneProx{
            get{
                return PagActual<TotalPaginas;
            }
        }
        public ListaPaginada(List<T> items,int cantidad,int nro,int tam){
            //inicializando
            TotalReg=cantidad;
            TamPag=tam;
            PagActual=nro;
            TotalPaginas=(int) Math.Ceiling(cantidad/ (double) tam );
            AddRange(items);
        }
        public static ListaPaginada<T> Paginar(IQueryable<T> entidades,int nropag = 1,int tampag = 3){
            var resultado=entidades.Skip((nropag -1) * tampag ).Take(tampag).ToList();
            var cantidad=entidades.Count();
            return new ListaPaginada<T>(resultado,cantidad,nropag,tampag);
        }
    }
}