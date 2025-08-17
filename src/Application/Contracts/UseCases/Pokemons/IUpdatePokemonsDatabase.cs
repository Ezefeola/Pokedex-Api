namespace Application.Contracts.UseCases.Pokemons
{
    public interface IUpdatePokemonsDatabase
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}