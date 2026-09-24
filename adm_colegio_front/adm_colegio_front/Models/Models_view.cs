using System.Text.Json.Serialization;

namespace adm_colegio_front.Models
{
    public class Persona
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("apellido")]
        public string Apellido {  get; set; } = string.Empty ;

        [JsonPropertyName("identificacion")]
        public string Identificacion {  get; set; } = string.Empty ;

        [JsonPropertyName("tipo_pesona")]
        public string Tipo_persona {  get; set; } = string.Empty ;
    }

    public class Materia
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty ;
    }

    public class Asigna_materia
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("materia_id")]
        public int Materia_id { get; set; }

        [JsonPropertyName("profesor_id")]
        public int Profesor_id { get; set; }

        [JsonPropertyName("materia")]
        public Materia? Materia { get; set; }

        [JsonPropertyName("profesor")]
        public Persona? Profesor { get; set; }
    }

    public class Matricula_calificacion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("anio")]
        public int Anio {  get; set; }

        [JsonPropertyName("alumno_id")]
        public int Alumno_id { get; set; }

        [JsonPropertyName("asigna_mat_prof_id")]
        public int Asigna_mat_prof_id { get; set; }

        [JsonPropertyName("calificacion")]
        public decimal Calificacion {  get; set; }

        [JsonPropertyName("alumno")]
        public Persona? Alumno { get; set; }

        [JsonPropertyName("asigna_materia")]
        public Asigna_materia? Asigna_materia { get; set; }
    }

    public class ReporteConsolidadoDto
    {
        [JsonPropertyName("anio")]
        public int Anio { get; set; }

        [JsonPropertyName("alumno")]
        public string Alumno { get; set; } = string.Empty;

        [JsonPropertyName("ident_alumno")]
        public string Ident_alumno { get; set; } = string.Empty;

        [JsonPropertyName("codigo_materia")]
        public string Codigo_materia { get; set; } = string.Empty;

        [JsonPropertyName("materia")]
        public string Materia { get; set; } = string.Empty;

        [JsonPropertyName("profesor")]
        public string Profesor { get; set; } = string.Empty;

        [JsonPropertyName("calificacion")]
        public decimal Calificacion { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = string.Empty;
    }
}
