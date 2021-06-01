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
	/// Interaction logic for PlaceholderTextbox.xaml
	/// </summary>
	public partial class PlaceholderTextbox : UserControl
	{

		public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextbox), new FrameworkPropertyMetadata(string.Empty));
		public string PlaceholderText { get { return GetValue(PlaceholderTextProperty).ToString(); } set { SetValue(PlaceholderTextProperty, value); } }


		public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register("InputText", typeof(string), typeof(PlaceholderTextbox), new FrameworkPropertyMetadata(string.Empty));
		public string InputText { get { return GetValue(InputTextProperty).ToString(); } set { SetValue(InputTextProperty, value); } }



		public PlaceholderTextbox()
		{
			DataContext = this;
			InitializeComponent();
		}
	}
}
