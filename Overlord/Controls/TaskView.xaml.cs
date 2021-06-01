using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Overlord.Logic;
using Overlord.Pages;
using Overlord.Util;

namespace Overlord.Controls
{
	/// <summary>
	/// Interaction logic for TaskView.xaml
	/// 
	/// A view that presents a task to the user, along with its category. Also a checkbox for
	/// marking a task as done, and buttons for editing and deleting the task.
	/// </summary>
	public partial class TaskView : UserControl
	{

		//The task to view.
		public UserTask Task { get; }

		private Popup popup;

		/// <summary>
		/// Create a TaskView with the given refresh event (can be null) and task to view.
		/// </summary>
		public TaskView(UserTask task, Popup popup)
		{
			InitializeComponent();
			Title.Text = task.Name;
			if (task.Goal != null) Category.Text = task.Goal.Name;
			else Category.Text = "";
			Task = task;

			DoneCheckBox.IsChecked = task.Done;
			this.popup = popup;
		}

		/// <summary>
		/// Called when the edit button is clicked. Opens a new Edit Task dialog.
		/// </summary>
		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			TaskEditForm dialog = new TaskEditForm(Task);
			popup.Child = dialog;
			popup.IsOpen = true;
		}

		/// <summary>
		/// Called when the delete button is clicked. Deletes this task.
		/// </summary>
		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			Task.Delete();
			this.FindLogicalParent<IRefreshable>()?.Refresh();
			//refresh?.Invoke();
		}

		/// <summary>
		/// Called when the checkbox is checked. Marks the task as done.
		/// </summary>
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			
		}

		/// <summary>
		/// Called when the checkbox is unchecked. Marks the task as not done.
		/// </summary>
		private void CheckBox_Unchecked(object sender, System.EventArgs e)
		{
			Task.Done = false;
			Task.Update();
		}

		private void CheckBox_Checked(object sender, System.EventArgs e)
		{
			Task.Done = true;
			Task.Update();
		}
	}
}
