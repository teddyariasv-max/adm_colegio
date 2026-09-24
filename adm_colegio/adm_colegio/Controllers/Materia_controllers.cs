using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;
using adm_colegio.Models;

namespace adm_colegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class materias_controller : ControllerBase
    {
        private readonly db_context _context;

        public materias_controller(db_context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> obtener_materias()
        {
            return await _context.materias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> obtener_materia_por_id(int id)
        {
            var materia = await _context.materias.FindAsync(id);
            if (materia == null) return NotFound();
            return materia;
        }

        [HttpPost]
        public async Task<ActionResult<Materia>> crear_materia(Materia materia)
        {
            _context.materias.Add(materia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(obtener_materia_por_id), new { id = materia.i_id }, materia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> actualizar_materia(int id, Materia materia)
        {
            if (id != materia.i_id) return BadRequest();
            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.materias.Any(e => e.i_id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminar_materia(int id)
        {
            var materia = await _context.materias.FindAsync(id);
            if (materia == null) return NotFound();

            _context.materias.Remove(materia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}