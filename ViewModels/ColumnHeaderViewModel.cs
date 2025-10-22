using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.ViewModels
{
	public class ColumnHeaderViewModel
	{
		public ColumnHeaderViewModel(string name, string title)
		{
			Name = name;
			Title = title;
		}

		public string Name { get; init; }
		public string Title { get; set; }

		public override string ToString() => Title;
	}
}
