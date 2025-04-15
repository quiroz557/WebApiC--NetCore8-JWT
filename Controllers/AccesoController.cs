using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Custom;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly DbpruebaContext _db;
        private readonly Utilidades _utilidades;

        public AccesoController(DbpruebaContext db, Utilidades utilidades)
        {
            _db = db;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse([FromBody] UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Nombre = objeto.nombre,
                Correo = objeto.correo,
                Clave = _utilidades.encriptarSHA256(objeto.clave)
            };

            await _db.Usuarios.AddAsync(modeloUsuario);
            await _db.SaveChangesAsync();

            if (modeloUsuario.IdUsuario > 0)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = true});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO objeto)
        {
            var usuarioEncontrado = await _db.Usuarios
                .Where(
                    u => u.Correo == objeto.correo &&
                    u.Clave == _utilidades.encriptarSHA256(objeto.clave)
                ).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "", msg = "No se encontró ningún usuario" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado!)});
        }
    }
}
