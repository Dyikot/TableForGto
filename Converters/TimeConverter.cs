using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TableForGto.Converters
{
	public class TimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((TimeOnly)value).ToLongTimeString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TimeOnly.TryParse((string)value, CultureInfo.InvariantCulture, out var val) ? val : TimeOnly.MinValue;
		}
	}
}
