using Overlord.Logic;
using Overlord.Logic.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Overlord.Util;
using System.Windows.Controls.Primitives;
using Overlord.Pages;

namespace Overlord.Controls
{
	/// <summary>
	/// Interaction logic for TaskEditForm.xaml
	/// </summary>
	public partial class TaskEditForm : UserControl
	{


		private UserTask task;

		public TaskEditForm(UserTask task)
		{
			InitializeComponent();
			this.task = task;

			TaskTitle.InputText = task.Name;
			TaskDueDate.SelectedDate = task.Time.Date;
			TaskDueTime.Time = task.Time.TimeOfDay;

			foreach(Goal g in ContextBuilder.Instance.Build().Goals)
			{
				SelectedGoal.Items.Add(g);
				if(task.Goal != null && g.Id == task.Goal.Id) SelectedGoal.SelectedItem = g;
			}
		}

		private void ConfirmButton_Click(object sender, RoutedEventArgs e)
		{
			task.Name = TaskTitle.InputText;
			task.Goal = SelectedGoal.SelectedItem as Goal;
			task.Time = (DateTime) TaskDueDate.SelectedDate + TaskDueTime.Time;

			using (TASKerContext context = ContextBuilder.Instance.Build()) {
				context.Update(task);
				context.SaveChanges();
			}

			this.ClosePopupAndRefreshParent();

		}
	}
}
