using System.Net.Http.Json;
using adm_colegio_front.Models;

namespace adm_colegio_front.Services;

public class Materia_service
{
    private readonly HttpClient _http;

    public Materia_service(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Materia_dto>> obtener_materias_async()
    {
        return await _http.GetFromJsonAsync<List<Materia_dto>>("api/materias") ?? new();
    }

    public async Task<bool> crear_materia_async(Materia_dto materia)
    {
        var response = await _http.PostAsJsonAsync("api/materias", materia);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> eliminar_materia_async(int id)
    {
        var response = await _http.DeleteAsync($"api/materias/{id}");
        return response.IsSuccessStatusCode;
    }
}