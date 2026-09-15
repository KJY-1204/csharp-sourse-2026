using System.Net;
using System.Net.Http.Json;
using Day08_DTO_Mapper.Data.DTOs;

namespace Day08_DTO_Mapper.Data.DataSources;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }
    }

    public async Task<PokemonDto?> GetPokemonAsync(string pokemonName)
    {
        var formattedName = pokemonName.Trim().ToLowerInvariant();
        var response = await _httpClient.GetAsync($"pokemon/{formattedName}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PokemonDto>();
    }
}