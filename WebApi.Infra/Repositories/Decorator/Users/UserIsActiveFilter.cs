using WebApi.Models.Shared.Decorator;
using WebApi.Infra.Repositories.Decorators.Users;

namespace WebApi.Infra.Repositories.Decorator.Users
{
    /// <summary>
    /// Decorator responsible for applying the "IsActive" filter
    /// on <see cref="UserBaseDecorator"/> queries.
    /// </summary>
    /// <remarks>
    /// This decorator checks whether the incoming request has an
    /// <c>IsActive</c> flag defined in its filter and, if so,
    /// restricts the query to users matching the specified status.
    /// </remarks>
    internal class UserIsActiveFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Applies the active/inactive filtering logic to the user query.
        /// </summary>
        /// <param name="request">
        /// The <see cref="UserBaseDecorator"/> containing the query and filters
        /// to be processed.
        /// </param>
        /// <returns>
        /// The same <see cref="UserBaseDecorator"/> instance, with the
        /// <c>IsActive</c> filter applied if defined.
        /// </returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            // If no filter is provided or IsActive is not specified,
            // return the request unmodified.
            if (request.Filter == null || !request.Filter.IsActive.HasValue)
                return request;

            // Apply filter: restricts results to users matching IsActive value.
            request.Query = request.Query.Where(e => e.IsActive == request.Filter.IsActive.Value);

            return request;
        }
    }
}
