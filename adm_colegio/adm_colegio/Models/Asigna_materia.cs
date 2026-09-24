using System.Text.Json.Serialization;

namespace adm_colegio.Models
{
    public class Asigna_materia
    {
        public int i_id { get; set; }
        public int i_profesor_id { get; set; }
        public int i_materia_id { get; set; }

        [JsonIgnore]
        public Persona? profesor { get; set; }

        [JsonIgnore]
        public Materia? Materia { get; set; }
    }
}