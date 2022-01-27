using System;
using System.Collections.Generic;

#nullable disable

namespace ApiParcial.Model
{
    public partial class Venta : ModeloBase
    {
        public int VentaId { get; set; }
        public string Patente { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Auto PatenteNavigation { get; set; }
    }
}
