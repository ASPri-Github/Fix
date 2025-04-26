using ApisConvenciones9.DTO.Responses;
using ApisConvenciones9.DTO.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApisConvenciones9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // POST: api/roles
        [HttpPost]
        [EndpointSummary("Crea un Rol de Usuario")]
        public async Task<ActionResult<RespuestaGeneralDTO>> CrearRol([FromBody] CrearRolDTO dto)
        {
            var respuesta = new RespuestaGeneralDTO
            {
                Message = []
            };

            if (await _roleManager.RoleExistsAsync(dto.Nombre))
            {
                respuesta.Message.Add("El rol ya existe");
                return respuesta;
            }
                
            var resultado = await _roleManager.CreateAsync(new IdentityRole(dto.Nombre));

            if (resultado.Succeeded)
            {
                respuesta.Message.Add("Rol creado");
                respuesta.Status = true;
                return respuesta;
            }
            else
            {
                var errores = TraducirErroresIdentity(resultado.Errors);
                respuesta.Message = errores;
            }

                return respuesta;

        }

        // PUT: api/roles/{id}
        [HttpPut("{id}")]
        [EndpointSummary("Actualiza el nombre de Rol de Usuario")]
        public async Task<ActionResult<RespuestaGeneralDTO>> EditarRol(string id, [FromBody] EditarRolDTO dto)
        {
            var respuesta = new RespuestaGeneralDTO
            {
                Message = []
            };

            var rol = await _roleManager.FindByIdAsync(id);
            if (rol == null)
            {
                respuesta.Message.Add("Rol no encontrado");

                return respuesta;
            }

            rol.Name = dto.NuevoNombre;
            var resultado = await _roleManager.UpdateAsync(rol);

            if (resultado.Succeeded)
            {
                respuesta.Message.Add("Rol actualizado");
                respuesta.Status = true;
                return respuesta;
            }
            else
            {
                var errores = TraducirErroresIdentity(resultado.Errors);
                respuesta.Message = errores;
            }

            return respuesta;
        
        }

        // GET: api/roles
        [HttpGet]
        [EndpointSummary("Obtiene los Roles de Usuario")]
        public async Task<ActionResult<RespuestaObjetoDTO>> ObtenerRoles()
        {
            var respuesta = new RespuestaObjetoDTO
            {
                Message = []
            };
            var roles =  await _roleManager.Roles.ToListAsync();

            if (roles == null)
            {
                respuesta.Message.Add("No se obtuvieron roles.");
            }
            else
            {
                respuesta.Status=true;
                respuesta.Message.Add($"{roles.Count} roles");
                respuesta.Response = roles;
            }
            return respuesta;
        }

        // POST: api/roles/asignar
        [HttpPost("asignar-rol")]
        [EndpointSummary("Asigna un rol a un Usuario")]
        public async Task<ActionResult> AsignarRolComoClaim([FromBody] AsignarRolDTO dto)
        {
            var usuario = await _userManager.FindByNameAsync(dto.UserName);
            if (usuario == null) return NotFound("Usuario no encontrado");

            var existe = await _roleManager.RoleExistsAsync(dto.Rol);
            if (!existe) return NotFound("Rol no existe");

            var claimsUsuario = await _userManager.GetClaimsAsync(usuario);
            if (claimsUsuario.Any(c => c.Type == "role" && c.Value == dto.Rol))
                return BadRequest("El usuario ya tiene ese rol como claim");

            await _userManager.AddClaimAsync(usuario, new Claim("role", dto.Rol));

            return NoContent();
        }

        //Eliminar rol
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarRol(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                return NotFound("Rol no encontrado.");
            }

            // Obtener nombre del rol para buscar usuarios
            var nombreRol = rol.Name;

            // Buscar todos los usuarios
            var usuarios = await _userManager.Users.ToListAsync();

            // Filtrar los que tienen el rol asignado
            var usuariosConRol = new List<IdentityUser>();

            foreach (var usuario in usuarios)
            {
                if (await _userManager.IsInRoleAsync(usuario, nombreRol))
                {
                    usuariosConRol.Add(usuario);
                }
            }

            // Quitar el rol de cada usuario
            foreach (var usuario in usuariosConRol)
            {
                var resultadoRemover = await _userManager.RemoveFromRoleAsync(usuario, nombreRol);

                if (!resultadoRemover.Succeeded)
                {
                    return BadRequest($"No se pudo quitar el rol al usuario {usuario.UserName}.");
                }
            }

            // Finalmente, eliminar el rol
            var resultadoEliminar = await _roleManager.DeleteAsync(rol);

            if (resultadoEliminar.Succeeded)
            {
                return Ok("Rol eliminado correctamente.");
            }

            var errores = resultadoEliminar.Errors.Select(e => e.Description).ToList();
            return BadRequest(errores);
        }


        [HttpGet("api/test-roles")]
        [EndpointSummary("obtiene un listado de roles de Usuario para realizar el test al endpoint")]
        public IActionResult TestRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        private List<string> TraducirErroresIdentity(IEnumerable<IdentityError> errores)
        {
            var lista = new List<string>();

            foreach (var error in errores)
            {
                string mensajeTraducido = error.Code switch
                {
                    "DuplicateRoleName" => "Ese nombre de rol ya existe.",
                    "InvalidRoleName" => "El nombre del rol no es válido.",
                    "TooShort" => "El nombre del rol es demasiado corto.",
                    _ => error.Description // fallback por si no lo conoces
                };

                lista.Add(mensajeTraducido);
            }

            return lista;
        }

    }
}