using System;
using System.Collections.Generic;

#nullable disable

namespace ApiParcial.Model
{
    public partial class Auto : ModeloBase
    {
        public Auto()
        {
            Venta = new HashSet<Venta>();
        }

        public string Patente { get; set; }
        public string Motor { get; set; }
        public int? Precio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
