using System;

namespace TableForGto.Models.ColumnTypes
{
    public interface IColumnType : IComparable, IComparable<IColumnType>
    {
		public string ToString();

        public object Value { get; }
	}
}
