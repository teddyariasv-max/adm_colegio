using System.Net.Http.Json;
using adm_colegio_front.Models;

namespace adm_colegio_front.Services;

public class Persona_service
{
    private readonly HttpClient _http;

    public Persona_service(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Persona_dto>> obtener_personas_async()
    {
        return await _http.GetFromJsonAsync<List<Persona_dto>>("api/personas") ?? new();
    }

    public async Task<bool> crear_persona_async(Persona_dto persona)
    {
        var response = await _http.PostAsJsonAsync("api/personas", persona);
        return response.IsSuccessStatusCode;
    }

    // Nuevo método para modificar con PUT
    public async Task<bool> actualizar_persona_async(int id, Persona_dto persona)
    {
        var response = await _http.PutAsJsonAsync($"api/personas/{id}", persona);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> eliminar_persona_async(int id)
    {
        var response = await _http.DeleteAsync($"api/personas/{id}");
        return response.IsSuccessStatusCode;
    }
}