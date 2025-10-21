using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TableForGto.Views;

namespace TableForGto.Commands
{
	public static class TableCommands
	{
		public static RoutedCommand AddIntColumnCommand { get; set; } 
			= new("AddIntColumn", typeof(MainWindow));
		public static RoutedCommand AddFloatColumnCommand { get; set; }
			= new("AddFloatColumn", typeof(MainWindow));
		public static RoutedCommand AddStringColumnCommand { get; set; }
			= new("AddStringColumn", typeof(MainWindow));
		public static RoutedCommand AddDateColumnCommand { get; set; }
			= new("AddDateColumn", typeof(MainWindow));
		public static RoutedCommand AddTimeColumnCommand { get; set; }
			= new("AddTimeColumn", typeof(MainWindow));
	}
}
