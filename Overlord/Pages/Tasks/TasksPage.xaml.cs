using Overlord.Controls;
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

namespace Overlord.Pages.Tasks
{
	/// <summary>
	/// Interaction logic for TasksPage.xaml
	/// </summary>
	public partial class TasksPage : Page, IRefreshable
	{
		public TasksPage()
		{
			InitializeComponent();

			Refresh();

		}



		public void Refresh()
		{

			TasksContainerToday.Children.Clear();
			TasksContainerTomorrow.Children.Clear();
			TasksContainerThisWeek.Children.Clear();
			TasksContainerLater.Children.Clear();

			using var context = ContextBuilder.Instance.Build();
			var tasks = context.UserTasks.Select<UserTask, string>(t => t.Name);
			foreach (UserTask task in context.UserTasks)
			{
				if (task.Time < DateTime.Today) 
					continue;

				if (task.Time.Date == DateTime.Today)
					TasksContainerToday.Children.Add(new TaskView(task, NewTaskPopup));
				else if (task.Time.Date == DateTime.Today + TimeSpan.FromDays(1))
					TasksContainerTomorrow.Children.Add(new TaskView(task, NewTaskPopup));
				else if (task.Time.Date < DateTime.Today + TimeSpan.FromDays(7) && (int)task.Time.DayOfWeek > (int)DateTime.Today.DayOfWeek)
					TasksContainerThisWeek.Children.Add(new TaskView(task, NewTaskPopup));
				else if (task.Time > DateTime.Today)
					TasksContainerLater.Children.Add(new TaskView(task, NewTaskPopup));
			}
		}

		private void NewTaskButton_Click(object sender, RoutedEventArgs e)
		{
			NewTaskPopup.IsOpen = true;
			NewTaskPopup.Child = new TaskEditForm(new UserTask() { Time = (DateTime.Now + new TimeSpan(1,0,0)).Date  });
		}
	}
}
