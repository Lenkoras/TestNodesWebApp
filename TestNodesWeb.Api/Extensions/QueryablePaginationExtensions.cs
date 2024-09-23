using TestNodesWeb.Api.Common.Models;

namespace TestNodesWeb
{
    public static class QueryablePaginationExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationInfo pagination) =>
            query?.Skip(pagination.SkipCount)
                .Take(pagination.TakeCount) ?? throw new ArgumentNullException(nameof(query));
    }
}