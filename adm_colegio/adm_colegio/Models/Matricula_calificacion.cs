using System.Text.Json.Serialization;

namespace adm_colegio.Models
{
    public class Matricula_calificacion
    {
        public int i_id { get; set; }
        public int i_alumno_id { get; set; }
        public int i_materia_id { get; set; }
        public int i_anio_academico { get; set; }
        public decimal? d_calificacion_final { get; set; }

        [JsonIgnore]
        public Persona? Alumno { get; set; }

        [JsonIgnore]
        public Materia? Materia { get; set; }
    }
}