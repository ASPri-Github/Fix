using ApisConvenciones9.Data;
using ApisConvenciones9.DTO;
using ApisConvenciones9.Models;
using ApisConvenciones9.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApisConvenciones9.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //ejemplo de autorizacion por tipo claim [Authorize(Policy = "Admin")]
    public class ConvencionesController : ControllerBase
    {
        private IEventosRepository _repoEventos;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ConvencionesController(IEventosRepository repo, ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            _repoEventos = repo;

        }

        [HttpPost("CrearEvento")]
        public async Task<ActionResult> CreateEvento(EventoDTO model)
        {

            //var mess = _repoEventos.CrearEvento(model);
            var evento= mapper.Map<Evento>(model);
            context.Add(evento);
            await context.SaveChangesAsync();

            var eventoDTO = mapper.Map<EventoDTO>(model);

            return CreatedAtRoute("ObtenerEvento", new { id = evento.Id }, eventoDTO);
        }

        [HttpGet("GetEventos")]
        public async Task<IEnumerable<EventoDTO>> GetEventos()
        {
            var eventos = await context.Eventos.ToListAsync();
            var eventosDTO = mapper.Map<IEnumerable<EventoDTO>>(eventos);
            return eventosDTO;
        }

        [HttpGet("EventoXId/{id:int}", Name = "ObtenerEvento")]
        public async Task<ActionResult<EventoDTO>> GetEventoId (int id)
        {
            var evento = await context.Eventos.FirstOrDefaultAsync(x => x.Id == id);

            if (evento is null)
            {
                return NotFound();
            }

            var eventoDTO = mapper.Map<EventoDTO>(evento);

            return eventoDTO;
        }

        [HttpPost("ActualizarEvento/{id:int}")]
        public async Task<ActionResult> ActualizarEvento(int id, EventoDTO model)
        {
            var evento = mapper.Map<Evento>(model);
            evento.Id = id;
            context.Update(evento);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEvento (int id)
        {
            var registroABorrar = await context.Eventos.Where(x  => x.Id == id).ExecuteDeleteAsync();

            if(registroABorrar == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
