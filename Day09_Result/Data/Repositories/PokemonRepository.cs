using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _apiDataSource;

    public PokemonRepository(IPokemonApiDataSource apiDataSource)
    {
        _apiDataSource = apiDataSource ?? throw new ArgumentNullException(nameof(apiDataSource));
    }

    public async Task<Result<Pokemon>> GetPokemonByNameAsync(string pokemonName)
    {
        var dataSourceResult = await _apiDataSource.GetPokemonAsync(pokemonName);
        if (dataSourceResult.IsFailure)
        {
            return Result<Pokemon>.Failure(dataSourceResult.Error);
        }

        var pokemon = dataSourceResult.Value!.ToDomain();
        return Result<Pokemon>.Success(pokemon);
    }
}