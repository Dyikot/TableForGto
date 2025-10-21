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
			CreateColumns((TableViewModel)e.NewValue);
		}

		private void CreateColumns(TableViewModel table)
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


		private void AddColumn(object defaultValue, IValueConverter? converter = null)
		{
			var table = (TableViewModel)_dataGrid.DataContext;

			table.AddColumn(defaultValue, converter);
			CreateColumns(table);
		}

		private void AddIntColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			AddColumn(0, new IntConverter());
		}

		private void AddFloatColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			AddColumn(0.0f, new FloatConverter());
		}

		private void AddStringColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			AddColumn(string.Empty);
		}

		private void AddDateColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			AddColumn(DateOnly.MinValue, new DateConverter());
		}

		private void AddTimeColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			AddColumn(TimeOnly.MinValue, new TimeConverter());
		}		
    }
}