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
	/// Interaction logic for CheckBox.xaml
	/// </summary>
	public partial class CheckBox : UserControl
	{

		public event EventHandler Checked;
		public event EventHandler Unchecked;

		public bool IsChecked { get { return Icon.Visibility == Visibility.Visible; } set { Icon.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }

		public CheckBox()
		{
			InitializeComponent();
		}

		private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (IsChecked)
			{
				Unchecked(this, EventArgs.Empty);
				IsChecked = false;
			}
			else
			{
				Checked(this, EventArgs.Empty);
				IsChecked = true;
			}
		}

	}
}
