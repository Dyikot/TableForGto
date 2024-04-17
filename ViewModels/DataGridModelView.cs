using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.DirectoryServices;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using TableForGto.Commands;
using TableForGto.Controllers;
using TableForGto.Converters;
using TableForGto.Models;
using TableForGto.Models.Columns;
using TableForGto.Models.ColumnTypes;
using TableForGto.Windows;
using TableForGto.Windows.IOWindows;

namespace TableForGto.ViewModels
{
    public class DataGridModelView
	{
		public const int MainColumnsAmount = 3;

		public static readonly MainColumn[] DefaultMainColumns = new MainColumn[3]
		{
			new MainColumn
			{
				DisplayIndex = 0,
				Header = "Место",
				BindingPath = "Rating",
				BindingMode = BindingMode.OneWay
			},
			new MainColumn
			{
				DisplayIndex = 1,
				Header = "ФИО",
				BindingPath = "Fio"
			},
			new MainColumn
			{
				DisplayIndex = 2,
				Header = "Группа",
				BindingPath = "Group"
			}
		};

		public readonly DataGrid View;

		private readonly ObservableCollection<Student> _students;

		/// <summary>
		/// Представлется собой коллекцию в которой хранятся информация о ResultColumn.
		/// Использование лишь актуально при добавлении и инициализации столбцов.
		/// Не отслеживается изменение значения полей DiplayIndex и Header.
		/// Для получения обновленной информации следует воспользоватся ResultColumns.
		/// </summary>
		private List<ResultColumn> _resultColumns;

		private readonly ContextMenu? _columnContextMenu = 
			Application
			.Current
			.MainWindow
			.FindResource("ColumnContextMenu") as ContextMenu;

		public DataGridModelView(
			DataGrid dataGrid, 
			IEnumerable<ResultColumn>? resultColumns = null,
			MainColumn[]? mainColumns = null,
			IEnumerable<Student>? students = null
		)
		{
			View = dataGrid;
			
			_students = new ObservableCollection<Student>(
				students ?? 
				Enumerable.Empty<Student>()
			);
			_resultColumns = new List<ResultColumn>(
				resultColumns ?? 
				Enumerable.Empty<ResultColumn>()
			);
			View.ItemsSource = _students;

			InitializeMainColumns(mainColumns);
			InitializeResultColumns();
			LastSortedColumn = View.Columns.First();
		}

		public string NewColumnName => $"Столбец {MainColumnsAmount + ResultDataGirdColumns.Count }";

		public string NewColumnPath => $"Results[{AllResultsColumnsCount}]";

		public DataGridColumn LastSortedColumn { get; set; }

		public List<Student> Students
		{
			get
			{
				return _students
					.Select(student =>
					{
						return new Student()
						{
							Fio = student.Fio,
							Group = student.Group,
							Rating = student.Rating,
							Results = new ObservableCollection<IColumnType?>(
								student.Results
								.Where((column, i) => ResultDataGirdColumns.ContainsKey(i
									+ MainColumnsAmount)))
						};
					})
					.ToList();
			}
		}

		public List<ResultColumn> ResultColumns
		{
			get
			{
				// Выбор отображаемых столбцов, т.е не удаленных
				// И преобразование в тип ResultColumn
				var columns = View.Columns
					.Where((column, index) => index > 2 &&
											  column.Visibility == Visibility.Visible)
					.Select(column =>
					{
						var tableColumn = _resultColumns[View.Columns.IndexOf(column)
							- MainColumnsAmount];
						return new ResultColumn()
						{
							Type = tableColumn.Type,
							Header = (string)((DataGridColumnHeader)column.Header).Content,
							Width = column.Width.DisplayValue,
							DisplayIndex = column.DisplayIndex
						};
					})
					.Cast<Column>()
					.ToList();

				// Корректировка DiplayIndex
				int displayIndex = 0;
				columns.AddRange(MainColumns);

				return columns
					.Select((column, i) => (column, i))
					.OrderBy(tuple => tuple.column.DisplayIndex)
					.Select(tuple =>
					{
						tuple.column.DisplayIndex = displayIndex++;
						return tuple;
					})
					.OrderBy(tuple => tuple.i)
					.Select(tuple => tuple.column)
					.Where(column => column.GetType() == typeof(ResultColumn))
					.Cast<ResultColumn>()
					.ToList();
			}
		}

		public ReadOnlyCollection<Student> SortedStudents
		{
			get
			{
				Func<Student, object?> orderProperty = 
					View.Columns.IndexOf(LastSortedColumn) switch
				{
					0 => student => student.Rating,
					1 => student => student.Fio,
					2 => student => student.Group,
					_ => student => student.Results[
						View.Columns.IndexOf(LastSortedColumn) - MainColumnsAmount]
				};

				return new (LastSortedColumn.SortDirection switch
				{
					ListSortDirection.Ascending => _students
						.OrderBy(orderProperty)
						.ToList(),
					ListSortDirection.Descending => _students
						.OrderByDescending(orderProperty)
						.ToList(),
					_ => _students.ToList()
				});
			}
		}

		public MainColumn[] MainColumns
		{
			get
			{
				return new MainColumn[3]
				{
					new MainColumn
					{
						Header = "Место",
						DisplayIndex = View.Columns[0].DisplayIndex,
						Width = View.Columns[0].Width.DisplayValue,
						BindingPath = "Rating",
						BindingMode = BindingMode.OneWay
					},
					new MainColumn
					{
						Header = "ФИО",
						DisplayIndex = View.Columns[1].DisplayIndex,
						Width = View.Columns[1].Width.DisplayValue,
						BindingPath = "Fio"
					},
					new MainColumn
					{
						Header = "Группа",
						DisplayIndex = View.Columns[2].DisplayIndex,
						Width = View.Columns[2].Width.DisplayValue,
						BindingPath = "Group"
					}
				};
			}
		}

		public ReadOnlyCollection<Student> SelectedStudents
		{
			get
			{
				return new ReadOnlyCollection<Student>(
					View.SelectedCells
					.GroupBy(cellInfo => cellInfo.Column)
					.First()
					.Select(cellInfo => cellInfo.Item)
					.Where(item => item is Student)
					.Cast<Student>()
					.ToList());
			}
		}

		public ReadOnlyCollection<DataGridColumn> SelectedColumns
		{
			get
			{
				return new ReadOnlyCollection<DataGridColumn>(
					View.SelectedCells
					.GroupBy(cellInfo => cellInfo.Column)
					.Select(group => group.Key)
					.ToList());
			}
		}

		public void ClearRating()
		{
			foreach (var student in _students)
			{
				student.Rating = null;
			}
		}

		public void MakeRating()
		{
			if(_students.Count == 0)
			{
				return;
			}

			if(ResultColumns.Count == 0)
			{
				ClearRating();
				return;
			}

			var allowedResultColumnsIndices = ResultDataGirdColumns
				.Select(tuple => tuple.Key - MainColumnsAmount)
				.Where(index => IsTypeAllowed(_resultColumns[index].Type))
				.ToList();

			// Представляет собой Dicionary, где
			// Key - индекс столбцов с результатами _resultColumns
			// Value - максимальное значение для каждого столбца (TimeColumn [сек])
			var columnsMaxValues = allowedResultColumnsIndices
				.Select(i =>
				{
					var student = _students.MaxBy(student => student.Results[i]);
					var columnType = _resultColumns[i].Type;
					var result = student.Results[i];

					if (result == null)
					{
						return (i, double.NaN);
					}

					if (columnType is TimeColumn)
					{
						return (i, GetTotalSeconds((TimeOnly)result.Value));
					}

					return (i, (double)result.Value);
				})
				.Where(tuple => !double.IsNaN(tuple.Item2))
				.ToDictionary(tuple => tuple.i, tuple => tuple.Item2);

			if(columnsMaxValues.Count == 0)
			{
				return;
			}

			double seconds, maxSeconds, numericValue, maxNumericValue;
			
			// Определение общего рейтинга для каждого student.
			var studentsAndRating = _students
				.Select(student =>
				{
					double rating = student.Results
						.Select((result, position) => (result, position))
						.Where(tuple => columnsMaxValues.ContainsKey(tuple.position))
						.Select(tuple =>
						{
							if(tuple.result == null)
							{
								return double.NaN;
							}

							if (tuple.result is TimeColumn)
							{
								seconds = GetTotalSeconds((TimeOnly)tuple.result.Value);
								maxSeconds = columnsMaxValues[tuple.position];

								return 1 - (seconds / maxSeconds);
							}
							else
							{
								numericValue = (double)tuple.result.Value;
								maxNumericValue = (double)columnsMaxValues[tuple.position];

								return numericValue / maxNumericValue;
							}
						})
						.Sum();

					return (student, rating);
				})
				.OrderByDescending(tuple => tuple.rating)
				.Select(tuple => tuple.student);

			// Выставление рейтинга в поле Rating для каждого студента
			int rating = 1;
			foreach(var student in studentsAndRating)
			{
				student.Rating = rating++;
			}

			static double GetTotalSeconds(TimeOnly time)
			{
				return (time.Hour * 60 * 60) + (time.Minute * 60) + time.Second + (double)time.Millisecond / 1000;
			}

			static bool IsTypeAllowed(IColumnType? columnType)
			{
				return columnType is NumericColumn ||
					   columnType is TimeColumn;
			}
		}

		public void AddColumn(IColumnType columnType)
		{
			var resultColumn = new ResultColumn
			{
				Type = columnType,
				Header = NewColumnName,
				DisplayIndex = View.Columns.Count,
			};

			_resultColumns.Add(resultColumn);

			foreach (var student in _students)
			{
				student.Results.Add(null);
			}

			AddResultColumn(resultColumn);
		}

		public void AddFromClipboard()
		{
			if (Clipboard.GetText() == null)
			{
				return;
			}

			var rowsData = ConvertToRowsData(Clipboard.GetText());

			var firstCellInfo = View.SelectedCells.First();
			int firstStudentIndex = firstCellInfo.Item is Student ?
				SortedStudents.IndexOf((Student)firstCellInfo.Item) :
				_students.Count;
			int firstColumnIndex = firstCellInfo.Column.DisplayIndex;

			int studentsToAddAmount = rowsData.Count - _students
				.Take(Range.StartAt(firstStudentIndex))
				.Count();
			studentsToAddAmount = studentsToAddAmount < 0 ? 0 : studentsToAddAmount;

			foreach (int i in Enumerable.Range(1, studentsToAddAmount))
			{
				_students.Add(new Student());
				InitializeNewStudent(_students.Last());
			}

			var studentsToPasteData = SortedStudents
				.Take(Range.StartAt(Index.FromStart(firstStudentIndex)))
				.ToList();

			var displayIndexToColumnIndex = View.Columns
				.Select((column, index) => (column.DisplayIndex, index))
				.ToDictionary(keySelector: tuple => tuple.DisplayIndex,
							  elementSelector: tuple => tuple.index);

			foreach(int row in Enumerable.Range(0, rowsData.Count))
			{
				foreach(int column in Enumerable.Range(firstColumnIndex,
													   rowsData[row].Count))
				{
					var cellValue = rowsData[row][column - firstColumnIndex];
					var student = studentsToPasteData[row];

					if(!displayIndexToColumnIndex.TryGetValue(column, out int columnIndex))
					{
						break;
					}

					switch (columnIndex)
					{
						case 0:
							student.Rating = int.TryParse(cellValue, out int result) ?
							result : null;
							break;
						case 1:
							student.Fio = cellValue;
							break;
						case 2:
							student.Group = cellValue;
							break;
						default:
							student.Results[columnIndex - MainColumnsAmount] = 
								NewInstanceOfColumn(
									_resultColumns[columnIndex - MainColumnsAmount],
									cellValue);
							break;
					}
				}
			}

			IColumnType? NewInstanceOfColumn(ResultColumn column, string? value)
			{
				switch (column.Type.GetType().Name)
				{
					case "NumericColumn":
						return NumericColumn.TryParse(value, out NumericColumn numericColumn) ?
							numericColumn : null;

					case "DateColumn":
						return DateColumn.TryParse(value, out DateColumn dateColumn) ?
							dateColumn : null;

					case "TextColumn":
						return value == null ? null : new TextColumn(value);

					case "TimeColumn":
						return TimeColumn.TryParse(value, out TimeColumn timeColumn) ? 
							timeColumn : null;

					default:
						return null;
				}
			}

			static List<List<string>> ConvertToRowsData(string clipboardText)
			{
				var rawRows = clipboardText.Split(
									separator: new string[] { "\r\n" },
									options: StringSplitOptions.RemoveEmptyEntries);

				return rawRows.Select(rawRow =>
				{
					var row = new List<string>();
					row.AddRange(rawRow.Split(new string[] { "\t" }, StringSplitOptions.None));
					return row;
				}).ToList();
			}
		}

		public void InitializeNewStudent(Student student)
		{
			foreach(var column in _resultColumns)
			{
				student.Results.Add(null);
			}

			if (_students.Count == 1)
			{
				for (int i = MainColumnsAmount; i < View.Columns.Count; i++)
				{
					var column = (DataGridTextColumn)View.Columns[i];

					column.Binding = new Binding($"Results[{i - MainColumnsAmount}]")
					{
						Converter = GetConventerByColumnType(
							_resultColumns[i - MainColumnsAmount].Type)
					};
				}
			}
		}

		public void RemoveStudents(ICollection<Student> students)
		{
			foreach(var student in students)
			{
				_students.Remove(student);
			}
		}

		public void ClearStudentsAtColumns(
			ICollection<Student> students, 
			ICollection<DataGridColumn> columns)
		{
			var columnIndices = columns.Select(column => View.Columns.IndexOf(column));

			foreach (var student in students)
			{
				foreach(int i in columnIndices)
				{
					switch (i)
					{
						case 0: 
							student.Rating = null;
							break;
						case 1:
							student.Fio = null;
							break;
						case 2:
							student.Group = null;
							break;
						default:
							student.Results[i - 3] = null;
							break;
					}
				}
			}
		}

		private void InitializeMainColumns(IEnumerable<MainColumn>? mainColumns)
		{
			mainColumns ??= DefaultMainColumns;
			foreach (var column in mainColumns)
			{
				View.Columns.Add(new DataGridTextColumn()
				{
					Header = new MyDataGridColumnHeader()
					{
						Content = column.Header
					},
					Binding = new Binding(column.BindingPath)
					{
						Mode = column.BindingMode
					},
					DisplayIndex = column.DisplayIndex,
					Width = column.Width == null ?
						new DataGridLength() :
						new DataGridLength((double)column.Width)
				});
			}
		}

		private int AllResultsColumnsCount => View.Columns.Count - MainColumnsAmount;

		/// <summary>
		/// Представляет собой Dictionary, где в качестве ключа - индекс DataGridColumn в
		/// коллекции _table.Columns.
		/// </summary>
		private Dictionary<int, DataGridColumn> ResultDataGirdColumns
		{
			get
			{
				return View.Columns
					.Take(Range.StartAt(MainColumnsAmount))
					.Where(column => column.Visibility == Visibility.Visible)
					.Select(column =>
					{
						int index = View.Columns.IndexOf(column);
						return (index, column);
					})
					.ToDictionary(tuple => tuple.index, tuple => tuple.column);
			}
		}

		private void InitializeResultColumns()
		{
			foreach (var column in _resultColumns)
			{
				AddResultColumn(column);
			}
		}

		private void AddResultColumn(ResultColumn column)
		{
			var columnHeader = new MyDataGridColumnHeader()
			{
				ContextMenu = _columnContextMenu,
				Content = column.Header,
			};

			columnHeader.ContextMenu?.CommandBindings.AddRange(
				new[]
				{
					new CommandBinding(
						command: TableCommands.RenameColumn,
						executed: RenameTableColumnExecuted
					),
					new CommandBinding(
						command: TableCommands.RemoveColumn,
						executed: RemoveTableColumnExecuted
					)
				}
			);

			var binding = new Binding(NewColumnPath)
			{
				Converter = GetConventerByColumnType(column.Type)
			};

			View.Columns.Add(new DataGridTextColumn
			{
				Header = columnHeader,
				Binding = binding,
				DisplayIndex = column.DisplayIndex,
				Width = column.Width != null ?
					new DataGridLength((double)column.Width) :
					new DataGridLength()
			});
		}

		private void RenameTableColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var contextMenu = (ContextMenu)sender;
			var columnHeader = (DataGridColumnHeader)contextMenu.PlacementTarget;

			var answer = InputBox.Show(
				messageBoxText: "Введите название столбца: ",
				caption: "Переименование столбца таблицы",
				value: (string)columnHeader.Content
			);

			if (answer != null)
			{
				columnHeader.Content = answer;
			}
		}

		private void RemoveTableColumnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var contextMenu = (ContextMenu)sender;
			var columnHeader = (DataGridColumnHeader)contextMenu.PlacementTarget;
			var column = columnHeader.Column;

			column.Visibility = Visibility.Collapsed;
		}

		private IValueConverter GetConventerByColumnType(IColumnType columnType)
		{
			return columnType.GetType().Name switch
			{
				"NumericColumn" => new StringToNumericColumnConverter(),
				"TextColumn" => new StringToTextColumnConverter(),
				"TimeColumn" => new StringToTimeColumnConverter(),
				"DateColumn" => new StringToDateColumnConverter(),
				_ => throw new NotImplementedException()
			};
		}
	}
}
