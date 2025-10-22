using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using TableForGto.Converters;
using TableForGto.ViewModels;

namespace TableForGto.DataTemplates
{
	public class ColumnsTemplate : ITemplate<IEnumerable<ColumnViewModel>, IEnumerable<DataGridTextColumn>>
	{
		public IEnumerable<DataGridTextColumn> Build(IEnumerable<ColumnViewModel> param)
		{
			return param
				.Select(c =>
				{
					var column = new DataGridTextColumn
					{
						Header = c.Header,
						Binding = c.NewBinding(),
						IsReadOnly = c.IsReadOnly
					};

					var widthBinding = new Binding("Width")
					{
						Source = c,
						Mode = BindingMode.OneWayToSource,
						Converter = new ColumnWidthConverter(),
						FallbackValue = c.Width
					};

					var indexBinding = new Binding("Order")
					{
						Source = c,
						Mode = BindingMode.OneWayToSource,
						FallbackValue = c.Order
					};

					BindingOperations.SetBinding(column, DataGridColumn.WidthProperty, widthBinding);
					BindingOperations.SetBinding(column, DataGridColumn.DisplayIndexProperty, indexBinding);

					return column;
				})
				.OrderBy(tc => tc.DisplayIndex);			
		}
	}
}
