using System;
using System.Globalization;
using System.Windows.Data;

namespace TableForGto.Converters
{
	public class FloatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return float.TryParse((string)value, CultureInfo.InvariantCulture, out var val) ? val : 0.0f;
		}
	}
}
