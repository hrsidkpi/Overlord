using Microsoft.EntityFrameworkCore;

namespace Overlord.Logic.Database.SQLite
{

	/// <summary>
	/// A TASKerContext connected to the local SQLite database.
	/// </summary>
	public class SQLiteTASKerContext : TASKerContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options) { 
			options.UseLazyLoadingProxies().UseSqlite("Data Source=Logic/Database/SQLite/Overlord.db"); 
		}


	}

}
