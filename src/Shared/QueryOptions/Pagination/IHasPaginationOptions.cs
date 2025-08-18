namespace Application.Utilities.QueryOptions.Pagination;
public interface IHasPaginationOptions
{
    int? PageIndex { get; }
    int? PageSize { get; }
}