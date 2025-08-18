using Application.Utilities.QueryOptions.Pagination;

namespace Shared.DTOs.Pokemons.Request;
public sealed record GetPokemonsRequestDto : IHasPaginationOptions
{
    public string ?Name { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public string? ImageUrl { get; set; } 
    public string? Type { get; set; }
    public bool? IsCaught { get; set; }

    public int? PageIndex { get; set; } 
    public int? PageSize { get; set; }
}