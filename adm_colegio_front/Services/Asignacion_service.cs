using System.Net.Http.Json;
using adm_colegio_front.Models;

namespace adm_colegio_front.Services;

public class Asignacion_service
{
    private readonly HttpClient _http;

    public Asignacion_service(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Asigna_materia_dto>> obtener_asignaciones_async()
    {
        return await _http.GetFromJsonAsync<List<Asigna_materia_dto>>("api/Asignaciones_") ?? new();
    }

    public async Task<(bool exito, string mensaje)> crear_asignacion_async(Asigna_materia_dto asignacion)
    {
        var response = await _http.PostAsJsonAsync("api/Asignaciones_", asignacion);
        if (response.IsSuccessStatusCode)
        {
            return (true, string.Empty);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }
    }

    public async Task<(bool exito, string mensaje)> eliminar_asignacion_async(int id)
    {
        var response = await _http.DeleteAsync($"api/Asignaciones_/{id}");
        if (response.IsSuccessStatusCode)
        {
            return (true, string.Empty);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }
    }
}