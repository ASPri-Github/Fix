using System.ComponentModel.DataAnnotations;

namespace ApisConvenciones9.DTO
{
    public class EditarClaimDTO
    {
        [Required]
        public required string UserName { get; set; }
    }
}
