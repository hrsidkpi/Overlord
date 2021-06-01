using Overlord.Logic;
using Overlord.Logic.Database;
using Overlord.Logic.Database.SQLite;
using Overlord.Util;
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

namespace Overlord.Controls
{
	/// <summary>
	/// Interaction logic for GoalDeleteForm.xaml
	/// </summary>
	public partial class GoalDeleteForm : UserControl
	{

		private Goal goal;
		private bool move;

		public GoalDeleteForm(Goal goal)
		{
			InitializeComponent();
			this.goal = goal;
			foreach (Goal g in ContextBuilder.Instance.Build().Goals)
			{
				if (g == goal) continue;
				SelectedGoal.Items.Add(g);
			}
			SelectedGoal.SelectedItem = SelectedGoal.Items[0];
			DeleteTasks.IsChecked = true;
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			using var context = SQLiteContextBuilder.Instance.Build();
			foreach (UserTask t in context.UserTasks.Where(t => t.Goal == goal))
			{
				if (move)
				{
					t.Goal = (Goal)SelectedGoal.SelectedItem;
					context.UserTasks.Update(t);
				}
				else
				{
					context.UserTasks.Remove(t);
				}
			}
			context.Goals.Remove(goal);
			context.SaveChanges();

			this.ClosePopupAndRefreshParent();
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if (sender == DeleteTasks)
			{
				move = false;
				MoveTasksLabel.Foreground = OverlordUtils.GetResource<SolidColorBrush>("Text");
				DeleteTasksLabel.Foreground = OverlordUtils.GetResource<SolidColorBrush>("TextHighlight");
				SelectedGoal.Visibility = Visibility.Hidden;
			}
			else
			{
				move = true;
				MoveTasksLabel.Foreground = OverlordUtils.GetResource<SolidColorBrush>("TextHighlight");
				DeleteTasksLabel.Foreground = OverlordUtils.GetResource<SolidColorBrush>("Text");
				SelectedGoal.Visibility = Visibility.Visible;
			}
		}
	}
}
