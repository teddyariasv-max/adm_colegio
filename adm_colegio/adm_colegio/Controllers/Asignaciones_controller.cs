using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;
using adm_colegio.Models;

namespace adm_colegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Asignaciones_controller : ControllerBase
    {
        private readonly db_context _context;

        public Asignaciones_controller(db_context context)
        {
            _context = context;
        }

        // GET: api/Asignaciones_
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asigna_materia>>> ObtenerAsignaciones()
        {
            try
            {
                return await _context.asignacion_materias_profesor
                    .Include(a => a.profesor)
                    .Include(a => a.Materia)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener asignaciones: {ex.Message}");
            }
        }

        // POST: api/Asignaciones_
        [HttpPost]
        public async Task<ActionResult<Asigna_materia>> CrearAsignacion([FromBody] Asigna_materia asignacion)
        {
            try
            {
                // 1. Validar que la materia exista
                var materiaExiste = await _context.materias.AnyAsync(m => m.i_id == asignacion.i_materia_id);
                if (!materiaExiste)
                {
                    return BadRequest("La materia especificada no existe.");
                }

                // 2. Validar que la persona exista y sea un Profesor ("P")
                var profesor = await _context.personas.FindAsync(asignacion.i_profesor_id);
                if (profesor == null || profesor.c_tipo_persona != "P")
                {
                    return BadRequest("La persona seleccionada no existe o no tiene el rol de Profesor.");
                }

                // 3. Validar duplicados (mismo profesor y misma materia)
                var existeAsignacion = await _context.asignacion_materias_profesor
                    .AnyAsync(a => a.i_profesor_id == asignacion.i_profesor_id && a.i_materia_id == asignacion.i_materia_id);

                if (existeAsignacion)
                {
                    return BadRequest("El profesor ya se encuentra asignado a esta materia.");
                }

                _context.asignacion_materias_profesor.Add(asignacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ObtenerAsignaciones), new { id = asignacion.i_id }, asignacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asignación: {ex.Message}");
            }
        }

        // DELETE: api/Asignaciones_/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAsignacion(int id)
        {
            try
            {
                var asignacion = await _context.asignacion_materias_profesor.FindAsync(id);
                if (asignacion == null)
                {
                    return NotFound($"No se encontró la asignación con ID {id}.");
                }

                _context.asignacion_materias_profesor.Remove(asignacion);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la asignación: {ex.Message}");
            }
        }
    }
}