using ApisConvenciones9.DTO;
using ApisConvenciones9.Models;
using AutoMapper;

namespace ApisConvenciones9.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDTO>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.NombreUsuario, config => config.MapFrom(ent => ent.UserName))
                .ReverseMap();
        }
    }
}
