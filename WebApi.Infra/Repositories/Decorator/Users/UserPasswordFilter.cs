using WebApi.Models.Shared.Decorator;

namespace WebApi.Infra.Repositories.Decorators.Users
{
    /// <summary>
    /// Decorator responsible for filtering users by password.
    /// </summary>
    public class UserPasswordFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Applies a password filter to the user query if a password is provided in the request.
        /// </summary>
        /// <param name="request">The user query decorator containing the query and filter criteria.</param>
        /// <returns>The modified <see cref="UserBaseDecorator"/> with the applied password filter.</returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            if (request.Filter == null || string.IsNullOrEmpty(request.Filter.Password))
                return request;

            request.Query = request.Query.Where(e => e.IdAuthentication.HasValue && e.Authentication != null && e.Authentication.Password == request.Filter.Password);

            return request;
        }
    }
}
