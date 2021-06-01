using Overlord.Logic.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Overlord.Logic
{
	/// <summary>
	/// A task the user has created with a name, due time, estimated duration and category.
	/// </summary>
	public class UserTask
	{

		//Database id.
		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; } //Name of task
		public DateTime Time { get; set; } //Due time
		//public int Duration { get; set; } //Duration in minutes


		public int DoneInt { get; set; }

		//Boolean wrapper property for DoneInt
		[NotMapped]
		public bool Done
		{
			get
			{
				return DoneInt == 1;
			}
			set
			{
				DoneInt = value ? 1 : 0;
			}
		}


		public virtual Goal Goal { get; set; }


		/// <summary>
		/// Empty constructor for EntityFramework.
		/// </summary>
		public UserTask()
		{

		}

		/// <summary>
		/// Create a user task
		/// </summary>
		/// <param name="name">Name of the task</param>
		/// <param name="time">Due time</param>
		/// <param name="categoryId">Category id</param>
		/// <param name="doneInt">0 if the task is not done, 1 if its done</param>
		public UserTask(string name, DateTime time, Guid categoryId, int doneInt)
		{
			Name = name;
			Time = time;
			//Duration = duration;
			DoneInt = doneInt;
		}

		/// <summary>
		/// Checks equality of this task with another object.
		/// They are equal if the object is another UserTask with the same Id.
		/// </summary>
		/// <param name="obj">Other object</param>
		/// <returns>True if the object is an identical UserTask. False otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is UserTask other)
			{
				return other.Id == Id;
			}
			return false;
		}

		/// <summary>
		/// Check equality between tasls
		/// </summary>
		/// <param name="t1">Task 1</param>
		/// <param name="t2">Task 2</param>
		/// <returns>True if the tasks are equal (same id), false otherwise.</returns>
		public static bool operator ==(UserTask t1, UserTask t2)
		{
			if (t1 is null)
			{
				if (t2 is null) return true;
				return false;
			}
			if (t2 is null) return false;

			return t1.Id == t2.Id;
		}

		/// <summary>
		/// Check inequality between two tasks
		/// </summary>
		/// <param name="t1">Task 1</param>
		/// <param name="t2">Task 2</param>
		/// <returns>True if the tasks are not equal (different IDs), false otherwise.</returns>
		public static bool operator !=(UserTask t1, UserTask t2)
		{
			return !(t1 == t2);
		}

		//Delete this task from the database.
		public void Delete()
		{
			using var db = ContextBuilder.Instance.Build();
			db.UserTasks.Attach(this);
			db.UserTasks.Remove(this);
			db.SaveChanges();
		}

		/// <summary>
		/// Update the task in the database.
		/// </summary>
		public void Update()
		{
			using var db = ContextBuilder.Instance.Build();
			if (db.UserTasks.Any(s => s.Id == Id))
				db.UserTasks.Update(this);
			else
				db.UserTasks.Add(this);
			db.SaveChanges();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Locale doesn't matter for hashes, it just needs to be pretty random")]
		public override int GetHashCode()
		{
			return Id.ToString().GetHashCode();
		}
	}
}
