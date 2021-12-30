using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiParcial.Model;
using ApiParcial.Repositorios;

namespace ApiParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControladorBase<Venta> 
    {
        private readonly VentasRepositorio<ConcesionariaContext> _context;

        public VentasController(VentasRepositorio<ConcesionariaContext> context):base(context)
        {
            _context = context;
        }

       
    }
}
