using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TableForGto.ViewModels;

namespace TableForGto.DataTemplates
{
	public class ColumnsTemplate : ITemplate<IEnumerable<ColumnViewModel>, IEnumerable<DataGridTextColumn>>
	{
		public IEnumerable<DataGridTextColumn> Build(IEnumerable<ColumnViewModel> param)
		{
			return param
				.Select(c => new DataGridTextColumn
				{
					Header = c.Header,
					DisplayIndex = c.Order,
					Binding = c.NewBinding(),
					IsReadOnly = c.IsReadOnly,
					Width = c.Width
				})
				.OrderBy(tc => tc.DisplayIndex);			
		}
	}
}
