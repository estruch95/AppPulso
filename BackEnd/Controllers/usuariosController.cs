using Microsoft.AspNetCore.Mvc;
using backPulso.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace backPulso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly AplicationDbContext _context; //contexto de la BBDD
        private readonly IConfiguration config; //configuración

        public usuariosController(AplicationDbContext context, IConfiguration _config)
        {
            _context = context;
            config = _config;
        }


        // GET: api/Usuarios/5
        [HttpGet("{user}/{password}")]
        public IActionResult GetUsuarios(string user, string password)
        {

            var usuario = new usuarios();
            usuario.Nombre = user;
            usuario.Password = password;

            //Validación del usuario
            if ((usuario.Nombre == "Pulso") && (usuario.Password == "pUlSo"))
            {
                var JWT_token = buildToken(usuario.Nombre);
                return Ok(JWT_token);
            }
            else {
                return NotFound("LOGIN INCORRECTO");
            }

        }

        private IActionResult buildToken(string user)
        {
            var secretKey = config.GetValue<string>("SecretKey"); //lectura de la llave secreta
            var key = Encoding.ASCII.GetBytes(secretKey); //encriptamos la llave secreta

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user)); //claims para identificar el usuario

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4), //tiempo de expiración del token (4 horas)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //encriptamos los datos de inicio de sesión en un SHA256
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string JWT_token = tokenHandler.WriteToken(createdToken); //Devuelve el token

            return Ok(JWT_token);
        }


    }
}
