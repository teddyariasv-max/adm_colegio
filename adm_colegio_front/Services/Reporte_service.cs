using System.Net.Http.Json;
using adm_colegio_front.Models;

namespace adm_colegio_front.Services;

public class Reporte_service
{
    private readonly HttpClient _http;

    public Reporte_service(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Reporte_calificacion_dto>> obtener_reporte_calificaciones_async()
    {
        return await _http.GetFromJsonAsync<List<Reporte_calificacion_dto>>("api/reportes_/calificaciones") ?? new();
    }
}