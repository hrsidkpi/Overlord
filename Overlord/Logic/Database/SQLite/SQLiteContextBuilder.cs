using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Overlord.Logic.Database.SQLite
{

	/// <summary>
	/// A builder for an SQLite TASKerContext.
	/// </summary>
	public class SQLiteContextBuilder : ContextBuilder
	{

		/// <summary>
		/// Return an SQLite TASKerContext (db connection).
		/// </summary>
		/// <returns>A TASKerContext connected to the SQLite database.</returns>
		public override TASKerContext Build()
		{
			var context = new SQLiteTASKerContext();
			return context;
		}
	}
}
