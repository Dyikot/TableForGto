using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TableForGto.Converters;

namespace TableForGto.ViewModels
{
	public abstract class ColumnViewModel
	{
		public required ColumnHeaderViewModel Header { get; set; }
		public required int Order { get; set; }
		public double Width { get; set; } = 150;
		public bool IsReadOnly { get; set; }

		public abstract Binding NewBinding();
	}

	public class MainColumnViewModel : ColumnViewModel
	{
		public override Binding NewBinding() => new Binding(Header.Name);
	}

	public class ResultColumnViewModel : ColumnViewModel
	{
		public required object DefaultValue { get; init; }
		public required IValueConverter? Converter { get; init; }
		public override Binding NewBinding() => 
			new Binding($"Results[{Header.Name}]") { Converter = Converter };
	}
}
