using Microsoft.VisualBasic.CompilerServices;
using Overlord.Controls;
using Overlord.Logic;
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

namespace Overlord.Pages.Tasks
{
	/// <summary>
	/// Interaction logic for GoalsPage.xaml
	/// </summary>
	public partial class GoalsPage : Page, IRefreshable
	{


		private Goal currentEditGoal = null;
		private Goal currentEditGoalParent = null;

		public GoalsPage()
		{
			InitializeComponent();
			Populate();
		}



		private Grid GetGoalView(Goal g)
		{
			Grid panel = new Grid() { Margin = new Thickness(10, 0, 10, 0), Background = new SolidColorBrush(Colors.Transparent) };

			Button b = new Button() { Margin = new Thickness(10), Style = OverlordUtils.GetResource<Style>("ButtonHoverEffect"), Content = g.Name, HorizontalAlignment = HorizontalAlignment.Left, FontSize = 15 };
			b.Click += (s, e) =>
			{
				currentEditGoal = g;
				currentEditGoalParent = null;
				NewGoalPopup.IsOpen = true;
				selectedGoalTypeToAdd = g.Type;
				NewGoalTypeLabel.Content = selectedGoalTypeToAdd.ToString();
				NewGoalNameTextbox.InputText = g.Name;

			};
			panel.Children.Add(b);

			Button rem = new Button() { Margin = new Thickness(10), Style = OverlordUtils.GetResource<Style>("ButtonHoverEffect"), Content = "X", HorizontalAlignment = HorizontalAlignment.Right, FontSize = 15 };
			rem.Click += (s, e) =>
			{
				DeleteGoalPopup.Child = new GoalDeleteForm(g);
				DeleteGoalPopup.IsOpen = true;
			};
			panel.Children.Add(rem);

			return panel;
		}

		private SubItemLadder GetSubGoalView(Goal g, Goal parent, bool first, double height)
		{
			SubItemLadder item = new SubItemLadder() { Margin = new Thickness(30, first ? 0 : -height / 2, 0, 0), Height = height, Width=350, HorizontalAlignment=HorizontalAlignment.Left };
			Grid panel = new Grid() { Margin = new Thickness(10, 0, 10, 0), Background = new SolidColorBrush(Colors.Transparent) };

			Button b = new Button() { Margin = new Thickness(0), Style = OverlordUtils.GetResource<Style>("ButtonHoverEffectGray"), Content = g.Name, HorizontalAlignment = HorizontalAlignment.Left };
			b.Click += (s, e) =>
			{
				currentEditGoal = g;
				NewGoalPopup.IsOpen = true;
				selectedGoalTypeToAdd = g.Type;
				NewGoalTypeLabel.Content = selectedGoalTypeToAdd.ToString();
				NewGoalNameTextbox.InputText = g.Name;

			};
			panel.Children.Add(b);

			Button rem = new Button() { Margin = new Thickness(10), Style = OverlordUtils.GetResource<Style>("ButtonHoverEffectGray"), Content = "X", HorizontalAlignment = HorizontalAlignment.Right };
			rem.Click += (s, e) =>
			{
				DeleteGoalPopup.Child = new GoalDeleteForm(g);
				DeleteGoalPopup.IsOpen = true;
			};
			panel.Children.Add(rem);

			item.Child = panel;
			return item;
		}

		public void Populate()
		{
			GoalsPanel.Children.Clear();

			List<Goal> longTermGoals = SQLiteContextBuilder.Instance.Build().Goals.Where(t => t.Type == GoalType.LongTerm).ToList();
			double height = 60;
			foreach (Goal g in longTermGoals)
			{
				GoalsPanel.Children.Add(GetGoalView(g));
				List<Goal> mediumTermGoals = SQLiteContextBuilder.Instance.Build().Goals.Where(t => t.Type == GoalType.MediumTerm && t.Parent == g).ToList();

				SubItemLadder addNewSubGoalButton = new SubItemLadder() { Height = height, Child = new Button() { HorizontalAlignment = HorizontalAlignment.Left, Style = OverlordUtils.GetResource<Style>("ButtonHoverEffect"), Content = "New Medium Term Goal", Margin = new Thickness(10, -3, 0, 0) } };
				((Button)addNewSubGoalButton.Child).Click += (s, e) =>
				{
					currentEditGoal = null;
					currentEditGoalParent = g;
					NewGoalPopup.IsOpen = true;
					selectedGoalTypeToAdd = GoalType.MediumTerm;
					NewGoalTypeLabel.Content = selectedGoalTypeToAdd.ToString();
					NewGoalNameTextbox.InputText = "";
				};
				if (mediumTermGoals.Count != 0)
				{
					GoalsPanel.Children.Add(GetSubGoalView(mediumTermGoals[0], g, true, height));
					foreach (Goal sub in mediumTermGoals.GetRange(1, mediumTermGoals.Count - 1))
					{
						GoalsPanel.Children.Add(GetSubGoalView(sub, g, false, height));
					}
					addNewSubGoalButton.Margin = new Thickness(30, -height / 2, 0, 0);
				}
				else
					addNewSubGoalButton.Margin = new Thickness(30, 0, 0, 0);
				GoalsPanel.Children.Add(addNewSubGoalButton);
			}
		}


		private GoalType selectedGoalTypeToAdd;

		private void MediumTermAddGoalButton_Click(object sender, RoutedEventArgs e)
		{
			currentEditGoal = null;
			NewGoalPopup.IsOpen = true;
			currentEditGoalParent = null;
			selectedGoalTypeToAdd = GoalType.MediumTerm;
			NewGoalTypeLabel.Content = selectedGoalTypeToAdd.ToString();
			NewGoalNameTextbox.InputText = "";
		}

		private void LongTermAddGoalButton_Click(object sender, RoutedEventArgs e)
		{
			currentEditGoal = null;
			currentEditGoalParent = null;
			NewGoalPopup.IsOpen = true;
			selectedGoalTypeToAdd = GoalType.LongTerm;
			NewGoalTypeLabel.Content = selectedGoalTypeToAdd.ToString();
			NewGoalNameTextbox.InputText = "";
		}

		private void NewGoalSubmit_Click(object sender, RoutedEventArgs e)
		{
			NewGoalPopup.IsOpen = false;
			bool isNew = false;
			if (currentEditGoal == null)
			{
				isNew = true;
				currentEditGoal = new Goal() { Name = NewGoalNameTextbox.InputText, Type = selectedGoalTypeToAdd, Parent=currentEditGoalParent };
			}
			else
			{
				currentEditGoal.Name = NewGoalNameTextbox.InputText;
			}
			using (var context = SQLiteContextBuilder.Instance.Build())
			{
				if (currentEditGoal.Parent != null) context.Goals.Attach(currentEditGoal.Parent);
				if (isNew)
					context.Goals.Add(currentEditGoal);
				else
					context.Goals.Update(currentEditGoal);
				context.SaveChanges();
			}
			Populate();
		}

		public void Refresh()
		{
			Populate();
		}
	}
}
