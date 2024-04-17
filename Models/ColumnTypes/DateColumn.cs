using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.PointOfService;

namespace TableForGto.Models.ColumnTypes
{
	public class DateColumn : IColumnType
	{
		private readonly DateOnly _value;

		public object Value => _value;

		public static DateColumn Parse(string s)
		{
			return new DateColumn(DateOnly.Parse(s));
		}

		public static bool TryParse(string? s, out DateColumn result)
		{
			if(DateOnly.TryParse(s, out DateOnly value))
			{
				result = new DateColumn(value);
				return true;
			}

			result = new DateColumn();
			return false;
		}

		public DateColumn()
		{
			_value = new DateOnly();
		}

		public DateColumn(DateOnly value)
		{
			_value = value;
		}

		public override string ToString() => _value.ToString();

		public int CompareTo(IColumnType? other)
		{
			return other is not null ? _value.CompareTo(other.Value) : -1;
		}

		public int CompareTo(object? obj)
		{			
			if(obj == null)
			{
				return -1;
			}

			if(obj is DateColumn column)
			{
				return _value.CompareTo(column.Value);
			}

			throw new ArgumentException();
		}
	}
}
