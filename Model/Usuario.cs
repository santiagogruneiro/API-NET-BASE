using System;
using System.Collections.Generic;

#nullable disable

namespace ApiParcial.Model
{
    public partial class Usuario : ModeloBase
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
    }
}
