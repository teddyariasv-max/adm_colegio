using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;
using adm_colegio.Models;

namespace adm_colegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class matriculas_controller : ControllerBase
    {
        private readonly db_context _context;

        public matriculas_controller(db_context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula_calificacion>>> obtener_matriculas()
        {
            return await _context.matricula_Calificacions
                .Include(m => m.Alumno)
                .Include(m => m.Materia)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Matricula_calificacion>> registrar_matricula(Matricula_calificacion matricula)
        {
            // Regla de Negocio 1: Rango de calificación (0.0 a 5.0)
            if (matricula.d_calificacion_final < 0.0m || matricula.d_calificacion_final > 5.0m)
            {
                return BadRequest("La calificación debe estar dentro del rango de 0.0 a 5.0.");
            }

            // Regla de Negocio 2: Un alumno no puede repetir materia en el mismo año académico
            bool yaMatriculado = await _context.matricula_Calificacions.AnyAsync(m =>
                m.i_alumno_id == matricula.i_alumno_id &&
                m.i_materia_id == matricula.i_materia_id &&
                m.i_anio_academico == matricula.i_anio_academico);

            if (yaMatriculado)
            {
                return BadRequest("El alumno ya se encuentra matriculado en esta materia para el mismo año académico.");
            }

            _context.matricula_Calificacions.Add(matricula);
            await _context.SaveChangesAsync();
            return Ok(matricula);
        }

        [HttpPut("{id}/calificacion")]
        public async Task<IActionResult> actualizar_calificacion(int id, [FromBody] decimal nuevaCalificacion)
        {
            // Regla de Negocio: Rango de calificación (0.0 a 5.0)
            if (nuevaCalificacion < 0.0m || nuevaCalificacion > 5.0m)
            {
                return BadRequest("La calificación debe estar dentro del rango de 0.0 a 5.0.");
            }

            var matricula = await _context.matricula_Calificacions.FindAsync(id);
            if (matricula == null) return NotFound();

            matricula.d_calificacion_final = nuevaCalificacion;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}