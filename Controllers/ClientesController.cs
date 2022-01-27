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
    public class ClientesController : ControladorBase<Cliente>
    {
        private readonly ClientesRepositorio<ConcesionariaContext> _repo;

        public ClientesController(ClientesRepositorio<ConcesionariaContext> repo):base(repo)
        {
            _repo = repo;
        }

        public override ActionResult<Cliente> Nuevo(Cliente entidad)
        {
            try
            {
                for (int i = 0; i < 50; i++)
                {
                    base.Nuevo(entidad);
                    entidad.Dni = (Convert.ToInt32(entidad.Dni) + 1).ToString();
                    entidad.ClienteId = 0;
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
            return Ok();
           
        }

    }
}
