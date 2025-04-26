using ApisConvenciones9.Data;
using ApisConvenciones9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApisConvenciones9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public HotelController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("NuevoHotel")]
        public async Task<ActionResult> CreateHotel(Hotel model)
        {
            context.Add(model);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetHoteles")]
        public async Task<IEnumerable<Hotel>> GetHotel()
        {
            return await context.Hoteles.ToListAsync();
        }

    }
}
