using WebApi.Models.Shared.Decorator;

namespace WebApi.Infra.Repositories.Decorators.Users
{
    /// <summary>
    /// Applies an email-based filter to the user query.
    /// </summary>
    public class UserEmailFilter : IBaseDecorator<UserBaseDecorator>
    {
        /// <summary>
        /// Filters users by email if a value is provided in the request filter.
        /// </summary>
        /// <param name="request">The base decorator containing the query and filters.</param>
        /// <returns>The updated decorator with the applied email filter.</returns>
        public override UserBaseDecorator Operation(UserBaseDecorator request)
        {
            // Verifica se o filtro existe e se o campo de email foi preenchido
            if (request.Filter == null || string.IsNullOrEmpty(request.Filter.Email))
                return request;

            var filter = request.Filter.Email;
            // Aplica o filtro pelo email informado
            request.Query = request.Query.Where(e =>  e.Email.Contains(filter));

            return request;
        }
    }
}
