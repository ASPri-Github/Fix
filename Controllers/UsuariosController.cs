using ApisConvenciones9.Data;
using ApisConvenciones9.DTO;
using ApisConvenciones9.DTO.Responses;
using ApisConvenciones9.Models;
using ApisConvenciones9.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;

namespace ApisConvenciones9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(UserManager<Usuario> userManager, 
            IConfiguration configuration, SignInManager<Usuario> signInManager,
            IServiciosUsuarios serviciosUsuarios, ApplicationDbContext context,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.serviciosUsuarios = serviciosUsuarios;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("lista-usuarios")]
        [Authorize]
        //[Authorize(Policy ="Admin")]
        [EndpointSummary("Obtiene la lista de todos los usuarios")]
        public async Task<RespuestaObjetoDTO> Get()
        {
            var usuarios = await context.Users.ToListAsync();
            var usuariosDTO = mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            var respuesta = new RespuestaObjetoDTO
            {
                Message =
                [
                    "Ok."
                ],
                Status = true,
                Response = usuariosDTO
            };
            return respuesta;
        }

        [HttpPost("registro")]
        [EndpointSummary("Api para registrar nuevos usuarios")]
        public async Task<ActionResult<RespuestaGeneralDTO>> Registrar(
            CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var respuesta = new RespuestaGeneralDTO
            { 
                Message = []
            };
            var usuario = new Usuario
            {
                UserName = credencialesUsuarioDTO.UserName,
                Email = credencialesUsuarioDTO.Email,
                Nombre = credencialesUsuarioDTO.Nombres,
                Apellidos = credencialesUsuarioDTO.Apellidos
            };

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDTO.Password!);

            if (resultado.Succeeded)
            {
                //var respuestaAutenticacion = await ConstruirToken (credencialesUsuarioDTO);
                respuesta.Status = true;
                respuesta.Message =
                [
                    "Usuario Creado."
                ];
                    
                return respuesta;
            }
            else
            {
                foreach (var error in resultado.Errors) 
                {
                    var customMessage = MapIdentityError(error);
                    respuesta.Message.Add(customMessage);
                }

                return respuesta;
            }
        }

        [HttpPost("login")]
        [EndpointSummary("Obtiene token al ingresar las credenciales correctas")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(
            CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var respuesta = new RespuestaAutenticacionDTO
            {
                Message = []
            };
            var usuario = await userManager.FindByNameAsync(credencialesUsuarioDTO.UserName!);
                //FindByEmailAsync(credencialesUsuarioDTO.Email);

            if(usuario is null)
            {                
                respuesta.Message =
                [
                    "El usuario no puede ser Nulo."
                ];
                return respuesta;
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario,
                credencialesUsuarioDTO.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var resp = await ConstruirToken(credencialesUsuarioDTO);
                resp.Status = true;
                resp.Message =
                [
                    "Login correcto."
                ];
                return resp;
            }
            else
            {
                respuesta.Message =
                [
                    "Revise el usuario o contraseña e intente de nuevo."
                ];
                return respuesta;
            }
        }

        [HttpPut]
        [Authorize]
        [EndpointSummary("Actualiza datos de Usuario")]
        public async Task<ActionResult<RespuestaGeneralDTO>> AddNomApeUsuario(ActualizarUsuarioDTO actualizarUsuarioDTO)
        {
            var respuesta = new RespuestaGeneralDTO
            {
                Message = []
            };
            var usuario = await userManager.FindByNameAsync(actualizarUsuarioDTO.NombreUsuario!);

            if (usuario is null)
            {
                respuesta.Message =
                [
                    "El usuario no fue encontrado."
                ];
                return respuesta;
            }

            usuario.Nombre = actualizarUsuarioDTO.Nombres;
            usuario.Apellidos = actualizarUsuarioDTO.Apellidos;
            usuario.Email = actualizarUsuarioDTO.Email;

            await userManager.UpdateAsync(usuario);

            respuesta.Status = true;
            respuesta.Message =
                [
                    "Datos del usuario actualizados."
                ];
            return respuesta;
        }

        [HttpGet("renovar-token")]
        [Authorize]
        [EndpointSummary("Renueva token de un usuario")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> RenovarToken()
        {
            var usuario = await serviciosUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return NotFound();
            }

            var credencialesUsuarioDTO = new CredencialesUsuarioDTO
            {
                Email = usuario.Email!,
                UserName = usuario.UserName!
            };

            var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);

            return respuestaAutenticacion;
        }

        [HttpPost("hacer-admin")]
        [Authorize(Policy = "Admin")]
        [EndpointSummary("Hacer Admin a un usuario.")]
        [EndpointDescription("Aplica permisos de admin a un usuario. Se necesita " +
            "usuario Admin para agregar permisos a otro usuario.")]
        public async Task<ActionResult> HacerAdmin (EditarClaimDTO editarClaimDTO)
        {
            var usuario = await userManager.FindByNameAsync(editarClaimDTO.UserName);

            if (usuario is null)
            {
                return NotFound();
            }

            await userManager.AddClaimAsync(usuario, new Claim("Admin", "true"));

            return NoContent();
        }

        [HttpPost("remover-admin")]
        [Authorize(Policy = "Admin")]
        [EndpointSummary("Remover Admin")]
        [EndpointDescription("Elimina permisos de Admin a un usuario.")]
        public async Task<ActionResult> RemoverAdmin(EditarClaimDTO editarClaimDTO)
        {
            var usuario = await userManager.FindByNameAsync(editarClaimDTO.UserName);

            if (usuario is null)
            {
                return NotFound();
            }

            await userManager.RemoveClaimAsync(usuario, new Claim("Admin", "true"));

            return NoContent();
        }

        //private ActionResult RetornarLoginIncorrecto()
        //{
        //    ModelState.AddModelError(string.Empty, "Login incorrecto");
        //    return ValidationProblem();
        //}

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(
            CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var claims = new List<Claim>
            {
                //new Claim("email", credencialesUsuarioDTO.Email),
                new Claim("NombreUsuario",credencialesUsuarioDTO.UserName!)
            };

            var usuario = await userManager.FindByNameAsync(credencialesUsuarioDTO.UserName!);
            var claimsDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration["LlaveJWT"]!));
            var credemciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256 );

            var expiracion = DateTime.UtcNow.AddDays(10);

            var TokenSeguridad = new JwtSecurityToken(issuer: null, audience: null,
                claims: claims, expires: expiracion, signingCredentials: credemciales);

            var token= new JwtSecurityTokenHandler().WriteToken(TokenSeguridad);

            return new RespuestaAutenticacionDTO 
            { 
                Token = token,
                Expiracion = expiracion
            };
        }

        private string MapIdentityError(IdentityError error)
        {
            switch (error.Code)
            {
                case "DuplicateUserName":
                    return "Este nombre de usuario ya está en uso.";
                case "DuplicateEmail":
                    return "Ya existe una cuenta registrada con este correo electrónico.";
                case "PasswordTooShort":
                    return "La contraseña es demasiado corta.";
                case "PasswordRequiresNonAlphanumeric":
                    return "La contraseña debe contener al menos un carácter no alfanumérico.";
                case "PasswordRequiresDigit":
                    return "La contraseña debe contener al menos un número.";
                case "PasswordRequiresUpper":
                    return "La contraseña debe contener al menos una letra mayúscula.";
                case "PasswordRequiresLower":
                    return "La contraseña debe contener al menos una letra minúscula.";
                default:
                    return error.Description; // Por defecto, usa el mensaje original
            }
        }

    }
}
