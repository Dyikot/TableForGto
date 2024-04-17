using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models.Columns
{
	public class Column
	{
		public string Header { get; set; } = "Столбец 1";

		public int DisplayIndex { get; set; } = 3;

		public double? Width { get; set; }
	}
}
