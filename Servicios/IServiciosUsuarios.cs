using ApisConvenciones9.Models;
using Microsoft.AspNetCore.Identity;

namespace ApisConvenciones9.Servicios
{
    public interface IServiciosUsuarios
    {
        Task<Usuario?> ObtenerUsuario();
    }
}