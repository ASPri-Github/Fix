using ApisConvenciones9.Models;

namespace ApisConvenciones9.Repositories
{
    public interface IEventosRepository
    {
        //string CrearEvento(Evento model);
        Task<string> CrearEvento(Evento model);
    }
}