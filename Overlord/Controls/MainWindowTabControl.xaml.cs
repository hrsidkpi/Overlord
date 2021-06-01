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
	/// Interaction logic for MainWindowTabControl.xaml
	/// </summary>
	public partial class MainWindowTabControl : UserControl
	{

		private MainWindow owner;
		public Page Page { get; set; }
		
		public MainWindowTabControl(MainWindow owner, Page page, string title)
		{
			InitializeComponent();
			TabNameLabel.Content = title;
			Page = page;
			this.owner = owner;
		}
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			owner.DeleteTab(this);
		}


		private bool selected = false;
		public void Select()
		{
			TabNameLabel.Foreground = Application.Current.TryFindResource("Detail") as SolidColorBrush;
			selected = true;
		}

		public void Deselect()
		{
			TabNameLabel.ClearValue(Button.ForegroundProperty);
			selected = false;
			if(!IsMouseOver) DeleteButton.Visibility = Visibility.Hidden;
		}

		private void TabNameLabel_Click(object sender, RoutedEventArgs e)
		{
			owner.LoadTab(this);
		}

		private void TabNameLabel_MouseEnter(object sender, MouseEventArgs e)
		{
			DeleteButton.Visibility = Visibility.Visible;
		}

		private void TabNameLabel_MouseLeave(object sender, MouseEventArgs e)
		{
			if(!selected) DeleteButton.Visibility = Visibility.Hidden;
		}

		public void ShowSeparator()
		{
			Separator.Visibility = Visibility.Visible;
			MainGrid.ColumnDefinitions.Last().Width = new GridLength(15);
		}

		public void HideSeparator()
		{
			Separator.Visibility = Visibility.Hidden;
			MainGrid.ColumnDefinitions.Last().Width = new GridLength(0);
		}
	}
}
