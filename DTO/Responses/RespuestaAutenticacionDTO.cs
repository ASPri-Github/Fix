namespace ApisConvenciones9.DTO.Responses
{
    public class RespuestaAutenticacionDTO: RespuestaGeneralDTO
    {
        public string? Token {  get; set; }
        public DateTime Expiracion {  get; set; }
    }
}
