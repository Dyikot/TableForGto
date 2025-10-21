using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.ViewModels
{
	public class ColumnHeaderViewModel
	{
		public ColumnHeaderViewModel(string name, string header)
		{
			Name = name;
			Content = header;
		}

		public string Name { get; init; }
		public string Content { get; set; }

		public override string ToString() => Content;
	}
}
