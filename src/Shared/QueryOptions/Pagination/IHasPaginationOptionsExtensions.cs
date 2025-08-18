namespace Application.Utilities.QueryOptions.Pagination;
public static class IHasPaginationOptionsExtensions
{
    public static int GetPageIndex(this IHasPaginationOptions options)
    {
        return options.PageIndex is > 0 ? options.PageIndex.Value : 1;
    }

    public static int GetPageSize(this IHasPaginationOptions options)
    {
        return options.PageSize is > 0 ? options.PageSize.Value : 10;
    }

    public static int GetTotalPages(this IHasPaginationOptions options, int count)
    {
        if (count <= 0) return 1;
        int totalPages = count / options.GetPageSize();
        if (count % options.GetPageSize() != 0) totalPages++;
        return totalPages;
    }
}