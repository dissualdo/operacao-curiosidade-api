using WebApi.Models.Enums; 
using WebApi.Models.Models.Users;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
 

namespace WebApi.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public static readonly LoggerFactory _myLoggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
         
        #region .: Data Set :.
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Authentication> Authentications { get; set; }
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey("Id");
            builder.Entity<Authentication>().HasKey("Id");

            builder
                .Entity<Authentication>()
                .Property(d => d.Profile)
                .HasConversion(new EnumToStringConverter<EProfile>());

            // [BUILDER_END]
        }
    }
}
