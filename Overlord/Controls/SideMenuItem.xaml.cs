using Overlord.Pages;
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
	/// Interaction logic for SideMenuItem.xaml
	/// </summary>
	public partial class SideMenuItem : UserControl, ITabbedPageTab<Page>
	{

		public static readonly DependencyProperty itemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(SideMenuItem), new FrameworkPropertyMetadata(string.Empty));

		public string ItemName { get { return GetValue(itemNameProperty).ToString(); } set { SetValue(itemNameProperty, value); } }

		Page ITabbedPageTab<Page>.Content => Tab;

		public ITabbedPage<Page> Owner { get; set; }

		public Page Tab { get; set; }


		public SideMenuItem()
		{
			DataContext = this;
			InitializeComponent();
		}


		public SideMenuItem(ITabbedPage<Page> owner, Page tab)
		{
			DataContext = this;
			InitializeComponent();

			Owner = owner;
			Tab = tab;
		}


		public void Select()
		{
			Icon.Fill = Application.Current.TryFindResource("TextHighlight") as SolidColorBrush;
			TitleButton.Foreground = Application.Current.TryFindResource("TextHighlight") as SolidColorBrush;
			HightlightLine.Visibility = Visibility.Visible;
		}

		public void Deselect()
		{
			Icon.Fill = Application.Current.TryFindResource("Text") as SolidColorBrush;
			TitleButton.ClearValue(ForegroundProperty);
			HightlightLine.Visibility = Visibility.Hidden;
		}

		private void TitleButton_Click(object sender, RoutedEventArgs e)
		{
			Owner.Select(this);
		}
	}
}
