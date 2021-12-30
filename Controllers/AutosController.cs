using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiParcial.Model;
using ApiParcial.Repositorios;
using Clase4.Helpers;
using Microsoft.VisualBasic;

namespace ApiParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutosController : ControladorBase<Auto>
    {
        public readonly AutosRepositorio<ConcesionariaContext> _context;

        private readonly int recordsPerPage = 2;
        public AutosController(AutosRepositorio<ConcesionariaContext> context):base(context)
        {
            _context = context;
        }

      

        [HttpGet]
        [Route("vendidos")]
        public async Task<ActionResult<IEnumerable<Auto>>> GetAutosVendidos()
        {
            return _context.FindVendidos();
        }





        [HttpGet]
        [Route("disponibles")]
        public async Task<ActionResult<IEnumerable<Auto>>> GetAutosDisponibles()
        {
            return _context.FindDisponibles();
        }


        public override ActionResult<Auto> Nuevo(Auto entidad)
        {
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    base.Nuevo(entidad);
                    int value = int.Parse(Strings.Right(entidad.Patente, 1));
                    int newValue = value > 8 ? value - 1 : value + 1;
                    entidad.Patente = entidad.Patente.Replace(value.ToString(), newValue.ToString());
                     
                }


            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
            return Ok();

        }







        public override ActionResult<Auto> Modificar(Auto auto)
        {
  
            try
            {
                var entity = _context.FindById(auto.Patente);
                if (entity != null)
                {
                    base.Modificar(auto);
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
