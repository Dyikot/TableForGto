using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TableForGto.ViewModels;

namespace TableForGto.Views
{
	public partial class RenameColumnDialog : Window
	{
		public RenameColumnDialog()
		{
			InitializeComponent();
			var vm = new RenameColumnDialogModel();
			vm.Submitted += OnSubmitted;
			DataContext = vm;
		}

		private void OnSubmitted(object? sender, EventArgs e)
		{
			DialogResult = true;
		}
	}
}
