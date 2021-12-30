using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using ApiParcial.Model;

namespace ApiParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ConcesionariaContext _context;
        public LoginController(IConfiguration config,ConcesionariaContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            IActionResult response = Unauthorized();
       
            Usuario usuariovalidado = _context.Usuarios.Where(u=>u.Nombre.ToLower() == usuario.Nombre.ToLower() && u.Contrasenia == usuario.Contrasenia).FirstOrDefault();
            if (usuariovalidado != null)
            {
                var tokenString = GenerarJWT(usuariovalidado);
                response = Ok(new {
                    token= tokenString,
                    datosusuario=usuariovalidado
                });
            }
            return response;
        }


        private string GenerarJWT(Usuario usuariovalidado )
        {
            var clavesecreta = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credenciales = new SigningCredentials(clavesecreta, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usuariovalidado.Nombre),
                new Claim("nombrecompleto",usuariovalidado.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credenciales
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
