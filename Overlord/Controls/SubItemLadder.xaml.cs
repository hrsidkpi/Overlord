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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Overlord.Controls
{
	/// <summary>
	/// Interaction logic for SubItemLadder.xaml
	/// </summary>
	/// 
	[ContentProperty(nameof(Child))]
	public partial class SubItemLadder : UserControl
	{

		public static readonly DependencyPropertyKey ChildProperty = DependencyProperty.RegisterReadOnly(
			nameof(Child),
			typeof(UIElement),
			typeof(SubItemLadder),
			new PropertyMetadata());

		public UIElement Child
		{
			get { return (UIElement)GetValue(ChildProperty.DependencyProperty); }
			set { SetValue(ChildProperty, value); ChildBorder.Child = value; }
		}

		public SubItemLadder()
		{
			InitializeComponent();
			ChildBorder.Child = Child;
		}
	}
}
