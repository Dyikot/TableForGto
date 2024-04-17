using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models.ColumnTypes
{
	public class TimeColumn : IColumnType
	{
		private readonly TimeOnly _value;

		public object Value => _value;

		public static TimeColumn Parse(string s)
		{
			TryParse(s, out TimeColumn result);
			return result;
		}

		public static bool TryParse(string? s, out TimeColumn result)
		{
			if (TimeOnly.TryParseExact(s, "m:s", out TimeOnly timeOnly) ||
				TimeOnly.TryParseExact(s, "s.f", out timeOnly) ||
				TimeOnly.TryParseExact(s, "mm:ss.f", out timeOnly) ||
				TimeOnly.TryParse(s, out timeOnly))
			{
				result = new TimeColumn(timeOnly);
				return true;
			}

			result = new TimeColumn();
			return false;
		}

		public TimeColumn()
		{
			_value = new TimeOnly();
		}

		public TimeColumn(TimeOnly value) 
		{
			_value = value;
		}

		public override string ToString()
		{
			return _value.Hour > 0 ? 
					_value.ToLongTimeString() : 
					_value.Millisecond == 0 ?
						_value.ToString("mm:ss") :
						_value.ToString("mm:ss.f");
		}

		public int CompareTo(IColumnType? other)
		{
			return other is not null ? _value.CompareTo(other.Value) : -1;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
			{
				return -1;
			}

			if (obj is TimeColumn column)
			{
				return _value.CompareTo(column.Value);
			}

			throw new ArgumentException();
		}
	}
}
