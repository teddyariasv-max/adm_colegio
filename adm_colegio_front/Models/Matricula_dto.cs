namespace adm_colegio_front.Models;

public class Matricula_dto
{
    public int i_id { get; set; }
    public int i_alumno_id { get; set; }
    public int i_materia_id { get; set; }
    public int i_anio_academico { get; set; }
    public decimal? d_calificacion_final { get; set; }

    // Propiedades de navegación devueltas por el Include del API
    public Persona_dto? alumno { get; set; }
    public Materia_dto? materia { get; set; }
    public Persona_dto? profesor { get; set; }
}