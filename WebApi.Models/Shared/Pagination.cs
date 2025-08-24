using System.Linq;
using System.Collections.Generic;

namespace WebApi.Models.Shared
{
    /// <summary>
    /// Provides extension methods for paginating collections.
    /// </summary>
    public static class Pagination
    {

        /// <summary>
        /// Paginates an <see cref="IQueryable{T}"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The queryable collection to be paginated.</param>
        /// <param name="page">The page number (starting from 1).</param>
        /// <param name="limit">The number of items per page.</param>
        /// <returns>A paginated subset of the collection.</returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> collection, int page, int limit)
        {
            var skipCount = limit * (page - 1);
            return collection.Skip(skipCount).Take(limit);
        }

        /// <summary>
        /// Paginates an <see cref="IOrderedEnumerable{T}"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The ordered enumerable collection to be paginated.</param>
        /// <param name="page">The page number (starting from 1).</param>
        /// <param name="limit">The number of items per page.</param>
        /// <returns>A paginated subset of the collection.</returns>
        public static IEnumerable<T> Paginate<T>(this IOrderedEnumerable<T> collection, int page, int limit)
        {
            var skipCount = limit * (page - 1);
            return collection.Skip(skipCount).Take(limit);
        }

        /// <summary>
        /// Paginates an <see cref="IEnumerable{T}"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The enumerable collection to be paginated.</param>
        /// <param name="page">The page number (starting from 1).</param>
        /// <param name="limit">The number of items per page.</param>
        /// <returns>A paginated subset of the collection.</returns>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, int page, int limit)
        {
            var skipCount = limit * (page - 1);
            return collection.Skip(skipCount).Take(limit);
        }

        /// <summary>
        /// Paginates a <see cref="List{T}"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="collection">The list to be paginated.</param>
        /// <param name="page">The page number (starting from 1).</param>
        /// <param name="limit">The number of items per page.</param>
        /// <returns>A paginated subset of the list as a new list.</returns>
        public static List<T> PaginateList<T>(this List<T> collection, int page, int limit)
        {
            var skipCount = limit * (page - 1);
            return collection.Skip(skipCount).Take(limit).ToList();
        }
    }
}
