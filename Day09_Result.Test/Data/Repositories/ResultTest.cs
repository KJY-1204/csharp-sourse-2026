using System.Threading.Tasks;
using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Repositories;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day09_Result.Test.Data.Repositories;

[TestClass]
public class PokemonRepositoryResultTests
{
    [TestMethod]
    public async Task GetPokemonByNameAsync_NotFound_ReturnsFailureResult()
    {
        // Arrange (404 Not Found를 반환하는 가짜 DataSource)
        var fakeDataSource = new FakeNotFoundDataSource();
        var repository = new PokemonRepository(fakeDataSource);

        // Act
        var result = await repository.GetPokemonByNameAsync("dittooo");

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        StringAssert.Contains(result.Error, "404 Not Found");
    }

    private class FakeNotFoundDataSource : IPokemonApiDataSource
    {
        public Task<Result<PokemonDto>> GetPokemonAsync(string pokemonName)
        {
            return Task.FromResult(Result<PokemonDto>.Failure($"포켓몬 '{pokemonName}'을(를) 찾을 수 없습니다. (404 Not Found)"));
        }
    }
}