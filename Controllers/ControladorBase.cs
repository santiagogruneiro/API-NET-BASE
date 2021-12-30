using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using ApiParcial.Model;
using ApiParcial.Repositorios;
using Clase4.Helpers;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApiParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControladorBase<T> : ControllerBase where T : ModeloBase
    {
        public readonly IRepository<T> _repo;
        public ControladorBase(IRepository<T> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public virtual ActionResult<List<T>> GetTodos(string orden, int nropag, int tampag)
        {
            try
            {
                var entidades = _repo.FindAll().AsQueryable();

                if (orden != null)
                {
                    entidades = Ordenador<T>.Ordenar(entidades, orden);
                }
                if (nropag != 0 && tampag != 0)
                {

                    var listapag = ListaPaginada<T>.Paginar(entidades, nropag, tampag);
                    var metadatos = new
                    {
                        ListaPaginada<T>.TotalPaginas,
                        ListaPaginada<T>.TotalReg,
                        ListaPaginada<T>.PagActual,
                        ListaPaginada<T>.TieneAnt,
                        ListaPaginada<T>.TieneProx,
                        ListaPaginada<T>.TamPag
                    };
                    Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadatos));
                    return Ok(listapag);
                }
                return Ok(entidades);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error en el servidor, vuelva a intentar en otro momento");
            }

        }

        [HttpGet("{id}")]
        public virtual ActionResult<T> GetPorId(string id)
        {
            var resultado = _repo.FindById(id);
            return Ok(resultado);
        }


        [HttpPost]
        public virtual ActionResult<T> Nuevo(T entidad)
        {
            try
            {
                _repo.Crear(entidad);
                return Ok();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
        [HttpPut]
        public virtual ActionResult<T> Modificar(T entidad)
        {
            try
            {
                _repo.Actualizar(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la entidad: {ex}");
            }
            return Ok();

        }
        [HttpDelete]
        public virtual ActionResult<T> Borrar(string id)
        {
            try
            {
                _repo.Borrar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
            return Ok();
        }

    }
}