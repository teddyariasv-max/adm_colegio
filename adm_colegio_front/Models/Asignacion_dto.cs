namespace adm_colegio_front.Models;

public class Asignacion_dto
{
    public int i_id { get; set; }
    public int i_profesor_id { get; set; }
    public int i_materia_id { get; set; }

    // Propiedades de navegación opcionales para mostrar los nombres en la tabla
    public Persona_dto? profesor { get; set; }
    public Materia_dto? materia { get; set; }
}