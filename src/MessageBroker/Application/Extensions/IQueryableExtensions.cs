using System.Collections;
using Application.Dtos;
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

public class PaginatedList<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEquatable<PaginatedList<T>>
{
    public int Count => Items.Length;
    public T[] Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }

    public T this[int index] => Items[index];

    /// <summary>
    /// Initializes a new instance of <see cref="PaginatedList"/>
    /// </summary>
    public PaginatedList()
    {
    }
    /// <summary>
    /// Initializes an empty PaginatedList.
    /// </summary>
    public PaginatedList(int capacity)
    {
        Items = new T[capacity];
        TotalCount = 0;
        PageNumber = 0;
        PageSize = 0;
        TotalPages = 0;
    }

    /// <summary>
    /// Initializes a PaginatedList from an existing enumerable.
    /// </summary>
    /// <param name="items">The collection of items.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginatedList(IEnumerable<T> items, int pageNumber = 0, int pageSize = 10)
    {
        Items = items?.ToArray() ?? throw new ArgumentNullException(nameof(items));
        TotalCount = Items.Length;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    /// <summary>
    /// Initializes a PaginatedList with explicit total count.
    /// </summary>
    /// <param name="items">The collection of items.</param>
    /// <param name="totalCount">The total number of records.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginatedList(T[] items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

    public bool Equals(PaginatedList<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return TotalCount == other.TotalCount &&
               PageNumber == other.PageNumber &&
               PageSize == other.PageSize &&
               Items.SequenceEqual(other.Items);
    }

    public override bool Equals(object? obj) => obj is PaginatedList<T> other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(TotalCount, PageNumber, PageSize, Items.Length);

    internal object ToPaginatedList(int page, int pageSize)
    {
        throw new NotImplementedException();
    }
}