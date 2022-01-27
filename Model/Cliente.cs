using System;
using System.Collections.Generic;

#nullable disable

namespace ApiParcial.Model
{
    public partial class Cliente :ModeloBase
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
