using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TableForGto.Models
{
	public class MyDataGridColumnHeader : DataGridColumnHeader
	{
		static MyDataGridColumnHeader()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(MyDataGridColumnHeader),
				new FrameworkPropertyMetadata(typeof(MyDataGridColumnHeader))
			);
		}

		public override string ToString()
		{
			return Content.ToString();
		}
	}
}
