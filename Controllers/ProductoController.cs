using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize] //Solamente van a poder acceder usuarios autorizados
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DbpruebaContext _dbpruebaContext;

        public ProductoController(DbpruebaContext dbpruebaContext)
        {
            _dbpruebaContext = dbpruebaContext;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _dbpruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista});
        }
    }
}
