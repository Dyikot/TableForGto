using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using TableForGto.DataTemplates;
using TableForGto.Models;
using TableForGto.ViewModels;

namespace TableForGto.Views
{
    public partial class MainWindow : Window
	{
		private readonly ColumnTemplate _columnTemplate = new();

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
			if(e.OldValue is TableViewModel table)
			{
				table.ColumnAdded -= OnColumnAdded;
				table.ColumnRemoved -= OnColumnRemoved;
			}

			table = (TableViewModel)e.NewValue;
			table.ColumnAdded += OnColumnAdded;
			table.ColumnRemoved += OnColumnRemoved;

			var columns = table.Columns
				.Select(_columnTemplate.Build)
				.OrderBy(c => c.DisplayIndex);
			
			_dataGrid.Columns.Clear();
			foreach (var column in columns)
			{
				_dataGrid.Columns.Add(column);
			}
		}

		private void OnColumnRemoved(object? sender, ColumnViewModel e)
		{
			var column = _dataGrid.Columns
				.First(c => (string)((DataGridColumnHeader)c.Header).Content == e.Title);
			_dataGrid.Columns.Remove(column);
		}

		private void OnColumnAdded(object? sender, ColumnViewModel e)
		{
			var column = _columnTemplate.Build(e);
			_dataGrid.Columns.Add(column);
		}
	}
}