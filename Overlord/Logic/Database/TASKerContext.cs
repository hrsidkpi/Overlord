using Microsoft.EntityFrameworkCore;

namespace Overlord.Logic.Database
{

	/// <summary>
	/// This object is a database connection, and stores DbSet objects for tables in the database.
	/// </summary>
	public abstract class TASKerContext : DbContext
	{
		public DbSet<UserTask> UserTasks { get; set; }
		public DbSet<Goal> Goals { get; set; }


		public TASKerContext()
		{
			Database.EnsureCreated();
			//DbContext.Configuration
		}

	}
}
