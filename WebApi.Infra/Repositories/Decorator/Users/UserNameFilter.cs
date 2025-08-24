using WebApi.Models.Shared.Decorator;
namespace WebApi.Infra.Repositories.Decorators.Users
{
    /// <summary>
    /// Decorator responsible for filtering users by username.
    /// </summary>
    public class UserNameFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Applies a username filter to the user query if a username is provided in the request.
        /// </summary>
        /// <param name="request">The user query decorator containing the query and filter criteria.</param>
        /// <returns>The modified <see cref="UserBaseDecorator"/> with the applied username filter.</returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            if (request.Filter == null || string.IsNullOrEmpty(request.Filter.UserName))
                return request;

            var filter = request.Filter.UserName;

            request.Query = request.Query.Where(e => e.Name.Contains(filter) );

            return request;
        }
    }
}
