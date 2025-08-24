using WebApi.Models.Dto;
using WebApi.Models.Models.Users;

namespace WebApi.Infra.Repositories.Decorators.Users
{
    /// <summary>
    /// Base decorator for filtering user queries.
    /// This class is responsible for applying user-related filters to a query.
    /// </summary>
    public class UserBaseDecorator
    {
        /// <summary>
        /// The user query that will be filtered.
        /// </summary>
        public IQueryable<User> Query { get; set; }

        /// <summary>
        /// The filter criteria used to refine the user query.
        /// </summary>
        public GetUserFilterDto Filter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBaseDecorator"/> class.
        /// </summary>
        /// <param name="query">The queryable collection of users.</param>
        /// <param name="filter">The filtering criteria.</param>
        public UserBaseDecorator(IQueryable<User> query, GetUserFilterDto filter)
        {
            Query = query;
            Filter = filter;
        }
    }
}
