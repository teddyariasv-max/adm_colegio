using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;
using adm_colegio.DTOs;

namespace adm_colegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class reportes_controller : ControllerBase
    {
        private readonly db_context _context;

        public reportes_controller(db_context context)
        {
            _context = context;
        }

        [HttpGet("calificaciones")]
        public async Task<ActionResult<IEnumerable<reporte_calificacion_dto>>> obtener_reporte_calificaciones()
        {
            var reporte = await (from m in _context.matricula_Calificacions
                                 join a in _context.personas on m.i_alumno_id equals a.i_id
                                 join amp in _context.asignacion_materias_profesor on m.i_materia_id equals amp.i_materia_id
                                 join mat in _context.materias on amp.i_materia_id equals mat.i_id
                                 join p in _context.personas on amp.i_profesor_id equals p.i_id
                                 select new reporte_calificacion_dto
                                 {
                                     i_matricula_id = m.i_id,
                                     i_anio_academico = m.i_anio_academico,
                                     c_identificacion_alumno = a.c_identificacion,
                                     c_nombre_alumno = $"{a.c_nombre} {a.c_apellido}",
                                     c_codigo_materia = mat.c_codigo,
                                     c_nombre_materia = mat.c_nombre,
                                     c_identificacion_profesor = p.c_identificacion,
                                     c_nombre_profesor = $"{p.c_nombre} {p.c_apellido}",
                                     d_calificacion_final = m.d_calificacion_final ?? 0.0m,
                                     c_aprobo = (m.d_calificacion_final ?? 0.0m) >= 3.0m ? "SI" : "NO"
                                 }).ToListAsync();

            return Ok(reporte);
        }
    }
}