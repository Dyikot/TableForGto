using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
	public class MenuItemModelView
	{
		public MenuItemHeader Header => _header;

		private readonly MenuItem _item;

		private readonly MenuItemHeader _header;

		public MenuItemModelView(MenuItem item, MenuItemHeader itemHeader)
		{
			_item = item;
			_header = itemHeader;

			_item.SetBinding(
				MenuItem.HeaderProperty,
				new Binding("Value") { Source = _header }
			);
		}
	}
}
