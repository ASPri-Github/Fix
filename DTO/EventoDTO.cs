namespace ApisConvenciones9.DTO
{
    public class EventoDTO
    {
        public string NombreConvencion { get; set; } = null!;
        public string Subtitulo { get; set; } = null!;
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Imagen { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string LugarDestino { get; set; } = null!;
    }
}
