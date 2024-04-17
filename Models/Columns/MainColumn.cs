using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TableForGto.Models.ColumnTypes;

namespace TableForGto.Models.Columns
{
	public class MainColumn : Column
	{
		public string BindingPath { get; set; } = string.Empty;

		public BindingMode BindingMode { get; set; } = BindingMode.Default;
	}
}
