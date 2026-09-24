using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adm_colegio.Models;

public class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int i_id { get; set; }

    public string c_identificacion { get; set; } = string.Empty;
    public string c_nombre { get; set; } = string.Empty;
    public string c_apellido { get; set; } = string.Empty;
    public int? i_edad { get; set; }
    public string? c_direccion { get; set; }
    public string? c_telefono { get; set; }
    public string c_tipo_persona { get; set; } = "A"; // "A" = Alumno, "P" = Profesor
}