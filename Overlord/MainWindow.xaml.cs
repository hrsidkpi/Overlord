using Overlord.Controls;
using Overlord.Logic.Database.SQLite;
using Overlord.Pages;
using Overlord.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Overlord
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private MainWindowTabControl selectedTab = null;

		public MainWindow()
		{
			InitializeComponent();
			_ = new SQLiteContextBuilder().Build();

			TabsPanel.Children.Add(new MainWindowTabControl(this, new TasksCategory(), "Tasks"));
			//TabsPanel.Children.Add(new MainWindowTabControl(this, new TasksCategory(), "People"));
			//TabsPanel.Children.Add(new MainWindowTabControl(this, new TasksCategory(), "Notes"));
			TabsPanel.Children.Last<MainWindowTabControl>().HideSeparator();

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//DesktopAttacher.SetAsDesktop(this);

			var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
			this.Left = 0;
			this.Top = 0;

			this.Width = desktopWorkingArea.Width;
			this.Height = desktopWorkingArea.Height;
		}


		public void LoadTab(MainWindowTabControl tab)
		{
			TabsPanel.Children.Last<MainWindowTabControl>().ShowSeparator();
			if (selectedTab != null) selectedTab.Deselect();
			MainFrame.Content = tab.Page;
			tab.Select();
			selectedTab = tab;
			TabsPanel.Children.Last<MainWindowTabControl>().HideSeparator();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//throw new NotImplementedException();
		}

		public void DeleteTab(MainWindowTabControl tab)
		{
			if (tab == selectedTab) MainFrame.Content = null;
			TabsPanel.Children.Remove(tab);

			TabsPanel.Children.Last<MainWindowTabControl>()?.HideSeparator();
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{

		}
	}
}
