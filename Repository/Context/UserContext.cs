using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        /// <summary>
        /// Gets or sets the user table.
        /// </summary>
        /// <value>
        /// The user table.
        /// </value>
        public DbSet<User> UserTable
        {
            get;set;
        }
    }
}
