using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Logic
{

	public enum GoalType
	{
		LongTerm, MediumTerm
	}

	public class Goal
	{

		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public virtual Goal Parent { get; set; }

		private int TypeInt { get; set; }

		public GoalType Type
		{
			get
			{
				switch (TypeInt)
				{
					case 0:
						return GoalType.MediumTerm;
					case 1:
						return GoalType.LongTerm;
					default:
						return GoalType.MediumTerm;
				}
			}
			set
			{
				switch (value)
				{
					case GoalType.MediumTerm:
						TypeInt = 0;
						break;
					case GoalType.LongTerm:
						TypeInt = 1;
						break;
				}
			}

		}


		public override string ToString()
		{
			return Name;
		}

	}
}
