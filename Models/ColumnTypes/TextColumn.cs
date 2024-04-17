using System;

namespace TableForGto.Models.ColumnTypes
{
	public class TextColumn : IColumnType
    {
        private readonly string _value;

		public object Value => _value;

		public TextColumn()
		{
			_value = string.Empty;
		}

		public TextColumn(string value)
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

			if (obj is TextColumn column)
			{
				return _value.CompareTo(column.Value);
			}

			throw new ArgumentException();
		}
	}
}
