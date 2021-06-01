using System;
using System.Windows;
using System.Windows.Controls;

namespace Overlord.Controls
{
	/// <summary>
	/// Interaction logic for TimePicker.xaml
	/// 
	/// This view presents an input view for entering hours and minutes, including 
	/// buttons for adding or subtracting 1 hour or 15 minutes.
	/// </summary>
	public partial class TimePickerView : UserControl
	{
		/// <summary>
		/// Create a time picker view.
		/// </summary>
		public TimePickerView()
		{
			InitializeComponent();
		}

		//Dependency and property for Hours.
		public static readonly DependencyProperty HoursProperty = DependencyProperty.Register("Hours", typeof(int), typeof(TimePickerView));
		public int Hours { get { return (int)GetValue(HoursProperty); } set { SetValue(HoursProperty, value); } }
		
		//Dependency and property for Minutes.
		public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes", typeof(int), typeof(TimePickerView));
		public int Minutes { get { return (int)GetValue(MinutesProperty); } set { SetValue(MinutesProperty, value); } }

		//Getter for the time this view represents (hours + minutes).
		public TimeSpan Time { get { return new TimeSpan(Hours, Minutes, 0); } set { Hours = value.Hours; Minutes = value.Minutes; } }

		/// <summary>
		/// Called when the add hour button is pressed. Adds 1 hour.
		/// </summary>
		private void HourPlus_Click(object sender, RoutedEventArgs e)
		{
			Hours++;
			Hours %= 24;
		}

		/// <summary>
		/// Called when the subtract hour button is pressed. Subtracts 1 hour.
		/// </summary>
		private void HourMinus_Click(object sender, RoutedEventArgs e)
		{
			Hours--;
			if (Hours < 0) Hours = 23;
		}

		/// <summary>
		/// Called when the add minute button is pressed. Adds 15 minutes.
		/// </summary>
		private void MinutePlus_Click(object sender, RoutedEventArgs e)
		{
			Minutes+=15;
			Minutes %= 60;
		}

		/// <summary>
		/// Called when the subtract minute button is pressed. Subtracts 15 minutes.
		/// </summary>
		private void MinuteMinus_Click(object sender, RoutedEventArgs e)
		{
			Minutes-=15;
			if (Minutes < 0) Minutes = 45;
		}
	}
}
