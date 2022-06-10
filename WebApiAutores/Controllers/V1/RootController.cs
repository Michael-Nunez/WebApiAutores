using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RootController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public RootController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet(Name = "ObtenerRoot")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var datosHateoas = new List<DatoHATEOAS>();

            var esAdmin = await _authorizationService.AuthorizeAsync(User, "esAdmin");

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }),
                                             descripcion: "self", metodo: "GET"));

            // Autor
            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerAutores", new { }),
                                             descripcion: "autores", metodo: "GET"));

            if (esAdmin.Succeeded)
            {
                // Autor
                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearAutor", new { }),
                                             descripcion: "autor-crear", metodo: "POST"));

                // Libro
                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearLibro", new { }),
                                                 descripcion: "libro-crear", metodo: "POST"));
            }

            return datosHateoas;
        }
    }
}
