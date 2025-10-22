using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TableForGto.DataTemplates;
using TableForGto.Models;
using TableForGto.ViewModels;

namespace TableForGto.Views
{
    public partial class MainWindow : Window
	{
		private readonly ColumnsTemplate _columnsTemplate = new();

		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();
		}

		private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			
		}

		private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			
		}

		private void CloseExecuted(object sender, ExecutedRoutedEventArgs e) => Close();

		private void OnTableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			UpdateColumns((TableViewModel)e.NewValue);
		}

		private void UpdateColumns(TableViewModel table)
		{
			var columns = _columnsTemplate.Build(table.Columns);

			_dataGrid.Columns.Clear();
			foreach (var column in columns)
			{
				_dataGrid.Columns.Add(column);
			}
		}

		private void AddColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var dialog = new AddColumnDialogWindow();

			if (dialog.ShowDialog() == true)
			{
				var table = (TableViewModel)_dataGrid.DataContext;
				var vm = (AddColumnModel)dialog.DataContext;
				table.AddColumn(vm.Title, vm.Format);
				UpdateColumns(table);
			}
		}

		private void AddRowsExecured(object sender, ExecutedRoutedEventArgs e)
		{
			var table = (TableViewModel)_dataGrid.DataContext;
			table.AddStudents(50);
		}
	}
}