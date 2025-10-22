using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TableForGto.Converters;
using TableForGto.DataTemplates;
using TableForGto.Models;
using TableForGto.ViewModels;
using Windows.Devices.HumanInterfaceDevice;

namespace TableForGto.Views
{
    public partial class MainWindow : Window
	{
		private readonly ColumnsTemplate _columnsTemaplate = new();

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

		private void OnColumnIndexChanged(object sender, DataGridColumnEventArgs e)
		{
			var table = (TableViewModel)_dataGrid.DataContext;
			var header = (ColumnHeaderViewModel)e.Column.Header;
			var column = table.Columns.First(c => c.Header.Name == header.Name);

			column.Order = e.Column.DisplayIndex;
		}

		private void OnTableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			UpdateColumns((TableViewModel)e.NewValue);
		}

		private void UpdateColumns(TableViewModel table)
		{
			var columns = _columnsTemaplate.Build(table.Columns);

			_dataGrid.Columns.Clear();
			foreach (var column in columns)
			{
				_dataGrid.Columns.Add(column);
			}
		}

		private void OnAddStudent(object sender, AddingNewItemEventArgs e)
		{
			var table = (TableViewModel)_dataGrid.DataContext;
			var columns = table.Columns.OfType<ResultColumnViewModel>();
			var student = new Student();

			foreach (var column in columns)
			{
				student.Results.Add(column.Header.Name, column.DefaultValue);
			}

			e.NewItem = student;
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
	}
}