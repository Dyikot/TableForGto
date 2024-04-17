using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TableForGto.Models.ColumnTypes;

namespace TableForGto.Converters
{
	public class StringToTextColumnConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is IColumnType column ? column.ToString() : string.Empty;
		}

		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new TextColumn((string)value);
		}
	}
}
