using Overlord.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Overlord.Util
{
	public static class Extensions
	{

		public static string GetMonthName(this DateTime dateTime)
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
		}

		public static int DayOfWeekNum(this DayOfWeek day)
		{
			return (int)day;
		}

		public static DateTime LastDayOfWeek(this DateTime day, DayOfWeek target)
		{
			while (day.DayOfWeek != target) day = day.AddDays(-1);
			return day;
		}

		public static DateTime NextDayOfWeek(this DateTime day, DayOfWeek target)
		{
			while (day.DayOfWeek != target) day = day.AddDays(1);
			return day;
		}

		public static (int row, int col) GetCellAt(this Grid grid, double x, double y)
		{
			int row = 0;
			int col = 0;
			double accumulatedHeight = 0.0;
			double accumulatedWidth = 0.0;

			foreach (var rowDefinition in grid.RowDefinitions)
			{
				accumulatedHeight += rowDefinition.ActualHeight;
				if (accumulatedHeight >= y)
					break;
				row++;
			}

			foreach (var columnDefinition in grid.ColumnDefinitions)
			{
				accumulatedWidth += columnDefinition.ActualWidth;
				if (accumulatedWidth >= x)
					break;
				col++;
			}

			return (row, col);

		}

		public static T FindVisualParent<T>(this FrameworkElement child, string name = null) where T : FrameworkElement
		{
			//get parent item
			FrameworkElement parentObject = VisualTreeHelper.GetParent(child) as FrameworkElement;

			//we've reached the end of the tree
			if (parentObject == null) 
				return null;

			//check if the parent matches the type we're looking for
			T parent = parentObject as T;
			if (parent != null &&  (name == null || (parent as Control).Name == name))
				return parent;
			else
				return parentObject.FindVisualParent<T>();
		}

		public static T FindLogicalParent<T>(this FrameworkElement child, string name = null)
		{
			//get parent item
			FrameworkElement parent = child.Parent as FrameworkElement;

			//we've reached the end of the tree
			if (parent == null)
				return default(T);

			//check if the parent matches the type we're looking for
			if (parent is T t && (name == null || (parent as Control).Name == name))
				return t;
			else
				return parent.FindLogicalParent<T>();
		}

		public static T Last<T>(this UIElementCollection collection) where T : UIElement
		{
			return (T) collection[collection.Count - 1];
		}

		public static void ClosePopupAndRefreshParent(this FrameworkElement element)
		{
			element.FindLogicalParent<IRefreshable>()?.Refresh();


			Popup p = element.FindLogicalParent<Popup>();
			if (p != null)
			{
				p.IsOpen = false;
				p.Child = null;
			}
		}

	}
}
