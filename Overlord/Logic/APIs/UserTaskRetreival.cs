using Overlord.Logic;
using Overlord.Logic.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TASKer.Logic.APIs
{

	/// <summary>
	/// Provides static methods for retreiving UserTasks in common use cases.
	/// </summary>
	public static class UserTaskRetreival
	{

		/// <summary>
		/// Get all tasks from the db with due date in a given time range
		/// </summary>
		/// <param name="start">The start of the range</param>
		/// <param name="end">The end of the range</param>
		/// <returns>A list of UserTasks in the range</returns>
		public static List<UserTask> GetUserTasksInRange(DateTime start, DateTime end)
		{
			List<UserTask> tasks = new List<UserTask>();
			using var db = ContextBuilder.Instance.Build();

			foreach (UserTask task in db.UserTasks.Where(t => t.Time > start && t.Time < end))
			{
				tasks.Add(task);
			}
			return tasks;
		}

		/// <summary>
		/// Get tasks from the db that have a due date in a specific date.
		/// </summary>
		/// <param name="date">The date to search for tassk on</param>
		/// <returns>A list of UserTasks in the day</returns>
		public static List<UserTask> GetUserTasksInDate(DateTime date)
		{
			List<UserTask> tasks = new List<UserTask>();
			using var db = ContextBuilder.Instance.Build();

			foreach (UserTask task in db.UserTasks.Where(t => t.Time.Date == date.Date))
			{
				tasks.Add(task);
			}
			return tasks;
		}

		/// <summary>
		/// Get all the tasks in the db.
		/// </summary>
		/// <returns>All the tasks in the db</returns>
		public static List<UserTask> GetAllTasks()
		{
			using var db = ContextBuilder.Instance.Build();
			return db.UserTasks.ToList();
		}

		/// <summary>
		/// Get a UserTask by name from the db.
		/// </summary>
		/// <param name="name">The name of the task to find.</param>
		/// <returns>A usertask with the given name or null if none found.</returns>
		public static UserTask GetTaskByName(string name)
		{
			using var db = ContextBuilder.Instance.Build();
			var query = db.UserTasks.Where(t => t.Name == name);
			return query.Any() ? query.First() : null;
		}

		public static List<UserTask> GetTasksByGoal(Goal goal)
		{
			using var db = ContextBuilder.Instance.Build();
			var query = db.UserTasks.Where(t => t.Goal == goal);
			return query.ToList();
		}

	}
}
