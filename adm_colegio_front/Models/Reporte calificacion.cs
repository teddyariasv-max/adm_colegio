namespace adm_colegio_front.Models;

public class Reporte_calificacion_dto
{
    public int i_matricula_id { get; set; }
    public int i_anio_academico { get; set; }
    public string c_identificacion_alumno { get; set; } = string.Empty;
    public string c_nombre_alumno { get; set; } = string.Empty;
    public string c_codigo_materia { get; set; } = string.Empty;
    public string c_nombre_materia { get; set; } = string.Empty;
    public string c_identificacion_profesor { get; set; } = string.Empty;
    public string c_nombre_profesor { get; set; } = string.Empty;
    public decimal d_calificacion_final { get; set; }
    public string c_aprobo { get; set; } = string.Empty;
}