using WebApi.Models.Shared.Decorator;
namespace WebApi.Infra.Repositories.Decorators.Users
{
    /// <summary>
    /// Decorator responsible for filtering users by login.
    /// </summary>
    public class UserLoginFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Applies a login filter to the user query if a login is provided in the request.
        /// </summary>
        /// <param name="request">The user query decorator containing the query and filter criteria.</param>
        /// <returns>The modified <see cref="UserBaseDecorator"/> with the applied login filter.</returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            // If the filter is null or the login is not provided, return the original request.
            if (request.Filter == null || string.IsNullOrEmpty(request.Filter.Login))
                return request;

            // Applies a filter to retrieve only users matching the provided login.
            request.Query = request.Query.Where(e => e.IdAuthentication.HasValue && e.Authentication != null &&  e.Authentication.Login == request.Filter.Login);

            return request;
        }
    }
}
