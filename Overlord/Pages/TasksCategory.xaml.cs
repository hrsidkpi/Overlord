using Overlord.Controls;
using Overlord.Pages.Tasks;
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

namespace Overlord.Pages
{
	/// <summary>
	/// Interaction logic for TasksPage.xaml
	/// </summary>
	public partial class TasksCategory : Page, ITabbedPage<Page>
	{
		public TasksCategory()
		{
			InitializeComponent();

			foreach(SideMenuItem item in SideMenu.Children)
			{
				item.Owner = this;
			}


			GoalsTab.Tab = new GoalsPage();
			TasksTab.Tab = new Overlord.Pages.Tasks.TasksPage();
		}

		private ITabbedPageTab<Page> selectedPage;
		public void Select(ITabbedPageTab<Page> page)
		{
			selectedPage?.Deselect();
			selectedPage = page;
			selectedPage.Select();
			if (selectedPage.Content is IRefreshable r) r.Refresh();
			TasksPageMainContent.Navigate(selectedPage.Content);
		}
	}
}
