using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;
using adm_colegio.Models;

namespace adm_colegio.Controllers
{
    [ApiController]
    [Route("api/personas")] // Ruta fija y limpia: api/personas
    public class personas_controller : ControllerBase
    {
        private readonly db_context _context;

        public personas_controller(db_context context)
        {
            _context = context;
        }

        // GET: api/personas  ó  api/personas?tipo=A
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> ObtenerPersonas([FromQuery] string? tipo)
        {
            try
            {
                var query = _context.personas.AsQueryable();

                if (!string.IsNullOrEmpty(tipo))
                {
                    query = query.Where(p => p.c_tipo_persona == tipo);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al consultar personas: {ex.Message}");
            }
        }

        // GET: api/personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> ObtenerPersonaPorId(int id)
        {
            var persona = await _context.personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound($"No se encontró la persona con ID {id}");
            }

            return persona;
        }

        // POST: api/personas
        [HttpPost]
        public async Task<ActionResult<Persona>> CrearPersona([FromBody] Persona persona)
        {
            if (string.IsNullOrWhiteSpace(persona.c_nombre) || string.IsNullOrWhiteSpace(persona.c_identificacion))
            {
                return BadRequest("El nombre y la identificación son obligatorios.");
            }

            try
            {
                _context.personas.Add(persona);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ObtenerPersonaPorId), new { id = persona.i_id }, persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar en base de datos: {ex.Message}");
            }
        }

        // PUT: api/personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPersona(int id, [FromBody] Persona persona)
        {
            if (id != persona.i_id)
            {
                return BadRequest("El ID de la URL no coincide con el objeto enviado.");
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.personas.Any(e => e.i_id == id))
                {
                    return NotFound($"La persona con ID {id} ya no existe.");
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar: {ex.Message}");
            }
        }

        // DELETE: api/personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPersona(int id)
        {
            try
            {
                var persona = await _context.personas.FindAsync(id);
                if (persona == null)
                {
                    return NotFound($"No existe persona con ID {id}");
                }

                // Validación de integridad: Si es Alumno, verificar matriculas
                if (persona.c_tipo_persona == "A")
                {
                    bool tieneMatriculas = await _context.matricula_Calificacions
                        .AnyAsync(m => m.i_alumno_id == id);

                    if (tieneMatriculas)
                    {
                        return BadRequest("No se puede eliminar el alumno porque tiene materias asignadas o matriculadas.");
                    }
                }

                _context.personas.Remove(persona);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar: {ex.Message}");
            }
        }
    }
}