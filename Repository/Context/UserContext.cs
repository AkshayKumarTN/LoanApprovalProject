namespace Repository.Context
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// UserContext Class
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext" /> class.
        /// </summary>
        /// <param name="options">Context Options</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets Field 'Users' of type DataBaseSet
        /// </summary>
        public DbSet<RegisterModel> Users { get; set; }
    }
}
