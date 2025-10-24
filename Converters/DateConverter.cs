using System;
using System.Globalization;
using System.Windows.Data;

namespace TableForGto.Converters
{
	public class DateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString()!;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DateOnly.TryParse((string)value, out var val) ? val : DateOnly.MinValue;
		}
	}
}
