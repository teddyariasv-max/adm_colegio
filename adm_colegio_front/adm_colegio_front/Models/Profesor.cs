namespace adm_colegio_front.Models
{
    public class Profesor
    {
        public int i_id { get; set; }
        public string c_identificacion { get; set; } = string.Empty;
        public string c_nombre { get; set; } = string.Empty;
        public string c_apellido { get; set; } = string.Empty;
        public int? i_edad { get; set; }
        public string c_tipo_persona { get; set; } = "P"; // "P" = Profesor
    }
}