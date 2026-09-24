using System.Net.Http.Json;
using adm_colegio_front.Models;

namespace adm_colegio_front.Services;

public class Matricula_service
{
    private readonly HttpClient _http;

    public Matricula_service(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Matricula_dto>> obtener_matriculas_async()
    {
        return await _http.GetFromJsonAsync<List<Matricula_dto>>("api/matriculas") ?? new();
    }

    public async Task<(bool exito, string mensaje)> crear_matricula_async(Matricula_dto matricula)
    {
        var response = await _http.PostAsJsonAsync("api/matriculas", matricula);
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

    public async Task<(bool exito, string mensaje)> actualizar_calificacion_async(int id, decimal? calificacion)
    {
        if (!calificacion.HasValue) return (false, "Debe ingresar una calificación.");

        // El backend espera [FromBody] decimal, mandamos el valor directamente
        var response = await _http.PutAsJsonAsync($"api/matriculas/{id}/calificacion", calificacion.Value);
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