using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models.ColumnTypes
{
	public class NumericColumn : IColumnType
	{
		private readonly double _value;

		public object Value => _value;

		public static NumericColumn Parse(string s)
		{
			return new NumericColumn(double.Parse(s));
		}

		public static bool TryParse(string? s, out NumericColumn result)
		{
			if (double.TryParse(s, out double value))
			{
				result = new NumericColumn(value);
				return true;
			}

			result = new NumericColumn();
			return false;
		}

		public NumericColumn()
		{
			_value = 0;
		}

		public NumericColumn(double value)
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
			if (obj == null)
			{
				return -1;
			}

			if (obj is NumericColumn column)
			{
				return _value.CompareTo(column.Value);
			}

			throw new ArgumentException();
		}
	}
}
