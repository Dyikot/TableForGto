using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models.Columns;

namespace TableForGto.Models
{
	public class Table
	{
		public string Title { get; set; }

		public ICollection<MainColumn> MainColumns { get; set; }

		public ICollection<ResultColumn> ResultColumns { get; set; }

		public ICollection<Student> Students { get; set; }

		public Table()
		{ 
			Title = string.Empty;
			MainColumns = new List<MainColumn>();
			ResultColumns = new List<ResultColumn>();
			Students = new List<Student>();
		}
	}
}
