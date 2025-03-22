using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query,
                                                                       int pageNumber,
                                                                       int pageSize,
                                                                       CancellationToken ctx = default!)
    {
        var count = await query.CountAsync(ctx);
        var items = await query.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToArrayAsync(ctx);
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var count = query.Count();
        var items = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToArray();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }


}

public class PaginatedList<T>
{
    public T[] Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }

    public PaginatedList(T[] items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}