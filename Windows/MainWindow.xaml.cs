using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TableForGto.Collections;
using TableForGto.Commands;
using TableForGto.Controllers;
using TableForGto.Models;
using TableForGto.Models.Columns;
using TableForGto.Models.ColumnTypes;
using TableForGto.Serializers;
using TableForGto.ViewModels;
using TableForGto.Windows.IOWindows;

namespace TableForGto.Windows
{
    public partial class MainWindow : Window
	{
		public DataGridModelViewCollection DataGridMVCollection { get; set; }

		public TableMenuModelView TableMenuMV { get; set; }

		public TableTitleModelView TableTitleMV { get; set; }

		public MenuItemModelView DeleteRowsMenuItemMV { get; set; }

		private DispatcherTimer _timer;

		private readonly ProjectInfo _projectInfo;

		public MainWindow(ProjectInfo projectInfo)
		{
			InitializeComponent();

			_projectInfo = projectInfo;
			var tables = TableSerializer.Deserialize(_projectInfo);

			DataGridMVCollection = new DataGridModelViewCollection();
			TableTitleMV = new TableTitleModelView(_tableTitle);
			TableMenuMV = new TableMenuModelView(_tableMenu, TableTitleMV, tables);
			TableMenuMV.CurrentItemChanged += OnCurrentTableItemChanged;
			DeleteRowsMenuItemMV = new MenuItemModelView(
				_deleteRowsMenuItem, 
				new MenuItemHeader("Строку")
			);

			if (tables.Count() == 0)
			{
				DataGridMVCollection.Add(CreateDataGrid());
				TableMenuMV.AddMenuItem();
			}
			else
			{
				foreach(var table in tables)
				{
					DataGridMVCollection.Add(
						CreateDataGrid(),
						table.ResultColumns,
						table.MainColumns.ToArray(),
						table.Students
					);
				}
			}

			_timer = new DispatcherTimer(
				new TimeSpan(0, 10, 0),
				DispatcherPriority.Normal,
				OnTimerTicks,
				Application.Current.Dispatcher
			);

			_timer.Start();
		}
		
		private void OnTimerTicks(object? sender, EventArgs e)
		{
			TableSerializer.Serialize(ConvertToTableCollection(), _projectInfo);
		}

		private void OnAddTableButtonClick(object sender, RoutedEventArgs e)
		{
			TableMenuMV.AddMenuItem();
			DataGridMVCollection.Add(CreateDataGrid());
		}

		private void OnTableMenuButtonRightClick(object sender, MouseButtonEventArgs e)
		{
			((Button)sender).Focus();
		}

		private void OnTableMenuButtonInit(object sender, EventArgs e)
		{
			TableMenuMV.AddButton((Button)sender);
		}

		private void OnCurrentTableItemChanged(object? sender, EventArgs e)
		{
			if (TableMenuMV.CurrentItem != null)
			{
				TableTitleMV.Title = TableMenuMV.CurrentItem.Name;
				DataGridMVCollection.MoveCurrentToPosition(TableMenuMV.CurrentPosition);
			}
		}

		private void RemoveTableCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var button = (Button)sender;	
			var tableMenuItem = (TableMenuItem)button.DataContext;

			if(tableMenuItem != null)
			{
				int tableIndex = TableMenuMV.Items.IndexOf(tableMenuItem);
				var table = DataGridMVCollection[tableIndex];

				TableMenuMV.RemoveMenuItem(tableMenuItem);
				DataGridMVCollection.Remove(table);
				_windowGrid.Children.Remove(table.View);
			}
		}

		private void RemoveTableCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
		{
			if (TableMenuMV.Items.Count > 1)
			{
				e.CanExecute = true;
			}
		}

		private void RenameTableCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var button = (Button)sender;
			var tableMenuItem = (TableMenuItem)button.DataContext;

			var answer = InputBox.Show(
				messageBoxText: "Введите название таблицы: ",
				caption: "Переименование таблицы",
				value: tableMenuItem.Name
			);

			if(answer != null)
			{
				tableMenuItem.Name = answer;
				button.Content = tableMenuItem.ViewName;			
			}
		}

		private void CopyTableCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var button = (Button)sender;
			var menuItem = (TableMenuItem)button.DataContext;
			int tableIndex = TableMenuMV.Items.IndexOf(menuItem);

			TableMenuMV.AddMenuItem(GenerateItemName(menuItem));
			DataGridMVCollection.Duplicate(tableIndex, CreateDataGrid());

			string GenerateItemName(TableMenuItem menuItem)
			{
				string itemName = $"{menuItem.Name} - Копия";
				int duplicatesAmount = TableMenuMV.Items.Count(item => item.Name == itemName);

				return duplicatesAmount > 0 ?
					$"{itemName} ({duplicatesAmount})" :
					itemName;
			}
		}

		private void OnTableTitleDoubleClick(object sender, MouseButtonEventArgs e)
		{
			TableTitleMV.MakeEditable();
		}

		private void OnTableTitleLostKeyboardFocus(
			object sender, 
			KeyboardFocusChangedEventArgs e)
		{
			SetCurrentTableTitleByEditing();
		}

		private void OnTableTitleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SetCurrentTableTitleByEditing();
			}
		}

		private void OnMainGridClick(object sender, MouseButtonEventArgs e)
		{
			_windowGrid.Focus();
		}

		private void SetCurrentTableTitleByEditing()
		{
			if (!TableTitleMV.IsReadOnly)
			{
				TableTitleMV.MakeReadOnly();
				TableMenuMV.CurrentItem.Name = TableTitleMV.Title;
			}
		}

		private void AddNumericColumnCommandExecuted(
			object sender, 
			ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.AddColumn(new NumericColumn());
		}

		private void AddTextColumnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.AddColumn(new TextColumn());
		}

		private void AddTimeColumnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.AddColumn(new TimeColumn());
		}

		private void AddDateColumnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.AddColumn(new DateColumn());
		}

		private void PasteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{ 
			DataGridMVCollection.Current.AddFromClipboard();
		}

		private void CutCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var dataGrid = DataGridMVCollection.Current.View;

			ApplicationCommands.Copy.Execute(dataGrid, dataGrid);
			DeleteSelectionExecuted(sender, e);
		}

		private void OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
		{
			DataGridMVCollection.Current.InitializeNewStudent((Student)e.NewItem);
		}

		private DataGrid CreateDataGrid()
		{
			var table = new DataGrid();
			table.InitializingNewItem += OnInitializingNewItem;
			table.Sorting += OnTableSorting;

			table.CommandBindings.AddRange(new List<CommandBinding>
			{
				new CommandBinding(
					ApplicationCommands.Delete,
					DeleteCommandExecute
				),

				new CommandBinding(
					ApplicationCommands.Paste,
					PasteCommandExecuted
				),

				new CommandBinding(
					ApplicationCommands.Cut,
					CutCommandExecuted
				),

				new CommandBinding(
					TableCommands.DeleteSelection,
					DeleteSelectionExecuted
				),

				new CommandBinding(
					TableCommands.DeleteRows,
					DeleteRowsCommandExecuted,
					DeleteRowsCommandCanExecute
				),
				new CommandBinding(
					TableCommands.CopyWithHeaders,
					CopyWithHeadersCommandExecuted
				)
			});		

			_windowGrid.Children.Add(table);

			return table;
		}

		private void OnTableSorting(object sender, DataGridSortingEventArgs e)
		{
			DataGridMVCollection.Current.LastSortedColumn = e.Column;
		}

		private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Close();			
		}

		private void DeleteSelectionExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.ClearStudentsAtColumns(
				DataGridMVCollection.Current.SelectedStudents,
				DataGridMVCollection.Current.SelectedColumns
			);
		}

		private void DeleteRowsCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection
				.Current
				.RemoveStudents(DataGridMVCollection.Current.SelectedStudents);
		}

		private void DeleteRowsCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			var selectedStudents = DataGridMVCollection.Current.SelectedStudents;

			if(selectedStudents.Count == 0)
			{
				return;
			}

			int first = DataGridMVCollection.Current.SortedStudents
				.IndexOf(selectedStudents.First()) + 1;
			int last = DataGridMVCollection.Current.SortedStudents
				.IndexOf(selectedStudents.Last()) + 1;

			if (first > last)
			{
				(first, last) = (last,  first);
			}

			DeleteRowsMenuItemMV.Header.Value = first != last ?
				$"Строки {first}-{last}" :
				$"Строку {first}";

			e.CanExecute = true;
		}

		private void DeleteCommandExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var selectedStudents = DataGridMVCollection.Current.SelectedStudents;
			var selectedColumns = DataGridMVCollection.Current.SelectedColumns;

			if(selectedColumns.Count == DataGridMVCollection.Current.ResultColumns.Count
										+ DataGridMVCollection.Current.MainColumns.Length
			)
			{
				DataGridMVCollection.Current.RemoveStudents(selectedStudents);
			}
			else
			{
				DeleteSelectionExecuted(sender, e);
			}
		}

		private void MakeRatingCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.MakeRating();
		}

		private void OnDeleteItemSubmenuOpen(object sender, RoutedEventArgs e)
		{
			DeleteRowsMenuItemMV.Header.Value = DeleteRowsMenuItemMV.Header.DefaultValue;
		}

		private void OnLeftScrollButtonClick(object sender, RoutedEventArgs e)
		{
			_tableMenuScrollViewer.PageLeft();
		}

		private void OnRightScrollButtonClick(object sender, RoutedEventArgs e)
		{
			_tableMenuScrollViewer.PageRight();
		}

		private void OnTableMenuLoaded(object sender, RoutedEventArgs e)
		{
			if (_tableMenu.Template.FindName("OverflowGrid", _tableMenu) 
				is FrameworkElement overflowGrid)
			{
				overflowGrid.Visibility = Visibility.Hidden;
			}

			if (_tableMenu.Template.FindName("ToolBarThumb", _tableMenu) 
				is FrameworkElement mainPanelBorder)
			{
				mainPanelBorder.Visibility = Visibility.Collapsed;
			}
			
		}

		private void OnTableMenuButtonPreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && sender is Button button)
			{
				DragDrop.DoDragDrop(
					button,
					new DataObject(DataFormats.Serializable, button.DataContext), 
					DragDropEffects.Move
				);
			}
		}

		private void OnTableMenuClick(object sender, MouseButtonEventArgs e)
		{
			if(e.LeftButton == MouseButtonState.Pressed && sender is Button button)
			{
				TableMenuMV.CurrentItem = button.DataContext as TableMenuItem;
			}
		}

		private void OnTableMenuGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			e.UseDefaultCursors = false;
			Mouse.SetCursor(Cursors.SizeAll);
			e.Handled = true;
		}

		private void OnTableMenuDrop(object sender, DragEventArgs e)
		{
			DataGridMVCollection.Move(
				oldIndex: DataGridMVCollection.CurrentPosition,
				newIndex: TableMenuMV.CurrentPosition
			);
		}

		private void SaveProjectCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			TableSerializer.Serialize(ConvertToTableCollection(), _projectInfo);
		}

		private IEnumerable<Table> ConvertToTableCollection()
		{
			return DataGridMVCollection.Select((dataGirdMV, i) =>
			{
				return new Table
				{
					Title = TableMenuMV.Items[i].Name,
					Students = dataGirdMV.Students,
					MainColumns = dataGirdMV.MainColumns,
					ResultColumns = dataGirdMV.ResultColumns
				};
			});
		}

		private void OpenCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog()
			{
				InitialDirectory = Environment.CurrentDirectory,
				Filter = "Расширение проекта (*.tgto)|*.tgto",
				Title = "Открытие проекта"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				var projectInfo = new ProjectInfo(openFileDialog.FileName);
				var projects = new ProjectCollection(ProjectSerializer.Deserialize());
				projects.Add(projectInfo);
				ProjectSerializer.Serialize(projects);

				Application.Current.MainWindow = new MainWindow(projectInfo);
				Application.Current.MainWindow.Show();
				Close();
			}
		}

		private void NewCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if(new StartWindow(OpenMode.NewProject).ShowDialog() == true)
			{
				Close();
			}
		}

		private void OnTableMenuButtonDragOver(object sender, DragEventArgs e)
		{
			var targetButton = (Button)sender;
			var targetMenuItem = (TableMenuItem)targetButton.DataContext;
			var sourceMenuItem = (TableMenuItem)e.Data.GetData(DataFormats.Serializable);
			var sourceButton = TableMenuMV.GetButtonByItem(sourceMenuItem);

			if (targetButton == sourceButton)
			{
				return;
			}

			if (targetButton.ActualWidth > sourceButton.ActualWidth &&
				e.GetPosition(targetButton).X < targetButton.ActualWidth 
												- sourceButton.ActualWidth
			)
			{
				return;
			}

			TableMenuMV.CurrentButton = null;
			TableMenuMV.MoveItemToOtherItemPlace(sourceMenuItem, targetMenuItem);
		}

		private void HelpCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			new HelpWindow().Show();
        }

		private void CopyWithHeadersCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var dataGrid = DataGridMVCollection.Current.View;

			dataGrid.SetValue(
				DataGrid.ClipboardCopyModeProperty, 
				DataGridClipboardCopyMode.IncludeHeader
			);

			ApplicationCommands.Copy.Execute(dataGrid, dataGrid);

			dataGrid.SetValue(
				DataGrid.ClipboardCopyModeProperty,
				DataGridClipboardCopyMode.ExcludeHeader
			);
		}

		private void ClearRatingCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			DataGridMVCollection.Current.ClearRating();
		}
	}
}