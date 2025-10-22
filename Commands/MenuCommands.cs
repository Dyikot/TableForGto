using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TableForGto.Views;

namespace TableForGto.Commands
{
	public static class MenuCommands
	{
		public static RoutedUICommand AddColumnCommand { get; set; } 
			= new("Столбец","AddColumn", typeof(MenuCommands));
		public static RoutedUICommand AddRowsCommand { get; set; }
			= new("Строки", "AddRows", typeof(MenuCommands));
	}
}
