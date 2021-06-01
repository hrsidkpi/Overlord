using System;
using System.Collections.Generic;
using System.Text;

namespace Overlord.Logic.Database
{

	/// <summary>
	/// An abstract class for builders of TASKerContext objects.
	/// Singleton - only one builder can exist in the application and it is used 
	/// to create contexts everywhere.
	/// </summary>
	public abstract class ContextBuilder
	{

		//The current instance
		public static ContextBuilder Instance { get; set; }

		/// <summary>
		/// Create a new ContextBuilder. Only works once. Sets the Instance.
		/// </summary>
		public ContextBuilder()
		{
			if (Instance != null)
				throw new Exception("ContextBuilder is a singleton and only one instance is allowed");
			Instance = this;
		}

		/// <summary>
		/// Build a TASKerContext.
		/// </summary>
		/// <returns>A TASKerContext for the db created by the current builder.</returns>
		public abstract TASKerContext Build();

	}
}
