using WebApi.Models.Shared.Decorator;

namespace WebApi.Infra.Repositories.Decorators.Users
{ /// <summary>
  /// Decorator responsible for filtering users by their unique identifier (ID).
  /// </summary>
    public class UserIdFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Applies filtering by user ID if a valid ID is provided in the filter.
        /// </summary>
        /// <param name="request">The base decorator containing the query and filters.</param>
        /// <returns>The updated decorator with the applied filter.</returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            // Validate if the filter is provided and contains a valid ID
            if (request.Filter == null || !request.Filter.Id.HasValue)
                return request;

            // Apply the ID filter to the query
            request.Query = request.Query.Where(e => e.Id == request.Filter.Id.Value);

            return request;
        }
    }
}
