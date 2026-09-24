using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adm_colegio.Models
{
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int i_id { get; set; }

        public string c_codigo { get; set; } = string.Empty;
        public string c_nombre { get; set; } = string.Empty;
    }
}