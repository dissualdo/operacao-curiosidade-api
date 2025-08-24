using Microsoft.EntityFrameworkCore;
using WebApi.Infra.Data;
using WebApi.Infra.Repositories.Decorator.Users;
using WebApi.Infra.Repositories.Decorators.Users;
using WebApi.Models.Dto;
using WebApi.Models.Models.Users;
using WebApi.Models.Repositories;
using WebApi.Models.Shared; 

namespace WebApi.Infra.Repositories
{
    /// <summary>
    /// Repository responsible for managing user data operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context instance.</param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        #region .: Filter :. 
        /// <summary>
        /// Retrieves a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The <see cref="User"/> object if found; otherwise, null.</returns>
        public async Task<User> GetByIdAsync(long id)
        {
            return await _context.Users.AsNoTracking()
                                       .Include(u => u.Authentication)
                                       .FirstAsync(x => x.Id == id);
        }

        /// <summary>
        /// Retrieves a list of users based on the provided filter criteria asynchronously.
        /// </summary>
        /// <param name="filter">The filtering criteria for retrieving users.</param>
        /// <returns>A paginated list of users matching the filter criteria.</returns>
        public async Task<IEnumerable<User>> GetByFilterAsync(GetUserFilterDto filter)
        {
            var result = _context.Users.AsNoTracking()
                                       .Include(x => x.Curiosity)
                                       .Include(x => x.Authentication)
                                       .Select(a => a);

            result = FilterDecorator(result, filter);

            return await result.OrderBy(e => e.Name)
                               .Paginate(filter.CurrentPage, filter.ItensQuantity)
                               .ToListAsync();
        }

        /// <summary>
        /// Retrieves a list of users based on the provided filter criteria asynchronously.
        /// </summary>
        /// <param name="filter">The filtering criteria for retrieving users.</param>
        /// <returns>A paginated list of users matching the filter criteria.</returns>
        public async Task<User?> GetByLoginAsync(GetUserFilterDto filter)
        {
            var result = _context.Users.AsNoTracking()
                                       .Include(x => x.Authentication)
                                       .Select(a => a);

            if(string.IsNullOrEmpty(filter.Password) || string.IsNullOrEmpty(filter.Login))
                throw new ArgumentNullException(nameof(filter.Password), "Password cannot be null or empty when filtering by login.");

            result = FilterDecorator(result, filter);

            return await result.FirstOrDefaultAsync();
        }


        /// <summary>
        /// Retrieves total of users based on the provided filter criteria asynchronously.
        /// </summary>
        /// <param name="filter">The filtering criteria for retrieving users.</param>
        /// <returns>A paginated list of users matching the filter criteria.</returns>
        public async Task<int> TotalByFilterAsync(GetUserFilterDto filter)
        {
            var result = _context.Users.AsNoTracking()
                                       .Include(x => x.Authentication)
                                       .Select(a => a);

            result = FilterDecorator(result, filter);

            return await result.CountAsync();
        }
        #endregion

        #region .: CRUD :.
        /// <summary>
        /// Adds a new user to the database asynchronously.
        /// </summary>
        /// <param name="domain">The user entity to add.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> AddAsync(User domain)
        {
            await _context.Users.AddAsync(domain);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user in the database asynchronously.
        /// </summary>
        /// <param name="domain">The user entity containing updated information.</param>
        /// <returns>The number of affected rows.</returns>
        /// <exception cref="AppException">Thrown if the user is not found.</exception>
        public async Task<int> UpdateAsync(User domain)
        {
            var entity = await _context.Users
                                       .Include(x => x.Authentication)
                                       .Include(x => x.Curiosity)
                                       .FirstOrDefaultAsync(u => u.Id == domain.Id);
            if (entity == null)
                throw new AppException(AppException.UserNotRegistered);

            // Update user details
            entity.Age = domain.Age;
            entity.Name = domain.Name;
            entity.Email = domain.Email;
            entity.Address = domain.Address;
            entity.IsActive = domain.IsActive;

            if (entity.Curiosity is not null && domain.Curiosity is not null) { 
                entity.Curiosity.Feelings = domain.Curiosity.Feelings;
                entity.Curiosity.OtherInfo = domain.Curiosity.OtherInfo;
                entity.Curiosity.Interests = domain.Curiosity.Interests;
            }

            // Update authentication details if provided
            if (domain.Authentication != null && entity.Authentication != null)
            {
                if (entity.Authentication.Login != domain.Authentication.Login)
                    entity.Authentication.Login = domain.Authentication.Login;
            }

            _context.Users.Update(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user from the database asynchronously.
        /// </summary>
        /// <param name="domain">The user entity to delete.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> DeleteAsync(User domain)
        {
            var entity = await _context.Users.FindAsync(domain.Id);
            if (entity == null)
                throw new AppException(AppException.UserNotRegistered);

            _context.Users.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        #endregion


        #region .: Method :.

        /// <summary>
        /// Applies filtering decorators to refine the user query based on the provided filter.
        /// </summary>
        /// <param name="query">The original user query.</param>
        /// <param name="filter">The filtering criteria.</param>
        /// <returns>A refined <see cref="IQueryable{User}"/> with applied filters.</returns>
        private IQueryable<User> FilterDecorator(IQueryable<User> query, GetUserFilterDto filter)
        {
            var userBase = new UserBaseDecorator(query, filter);

            // Apply filters in sequence based on available criteria
            userBase = new UserIdFilter().Operation(userBase);
            userBase = new UserNameFilter().Operation(userBase);
            userBase = new UserLoginFilter().Operation(userBase);
            userBase = new UserEmailFilter().Operation(userBase);
            userBase = new UserPasswordFilter().Operation(userBase);
            userBase = new UserIsActiveFilter().Operation(userBase);

            return userBase.Query;
        }
        #endregion
    }
}
