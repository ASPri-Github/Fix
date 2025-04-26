using ApisConvenciones9.Data;
using ApisConvenciones9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApisConvenciones9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvencionistasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ConvencionistasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("NuevoConvencionista")]
        public async Task<ActionResult> CreateConvencionista(Convencionista model)
        {           
            context.Add(model);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetConvencionistas")]
        public async Task<IEnumerable<Convencionista>> GetConvencionistas()
        {
            return await context.Convencionistas.ToListAsync();
        }

        [HttpGet("ConvencionistaXId/{id:int}")]
        public async Task<ActionResult<Convencionista>> ConvencionistaXId(int id)
        {
            var convencionista = await context.Convencionistas.FirstOrDefaultAsync(x => x.Id == id);

            if (convencionista is null)
            {
                return NotFound();
            }

            return convencionista;
        }

        [HttpPost("ActualizarConvencionista/{id:int}")]
        public async Task<ActionResult> ActualizarConvencionista(int id, Convencionista model)
        {
            if (id != model.Id)
            {
                return BadRequest("Los ids deben concidir");
            }

            context.Update(model);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteConvecionista(int id)
        {
            var registroABorrar = await context.Convencionistas.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registroABorrar == 0)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
