namespace ApisConvenciones9.Models
{
    public class Version_App
    {
        public int Id { get; set; }
        public string Version_Android { get; set; } = null!;
        public string Version_IOs { get; set; } = null!;
        public string Token_Map_Box { get; set; } = null!;
        public DateTime Fecha { get; set; }
        //usuario_id
    }
}
