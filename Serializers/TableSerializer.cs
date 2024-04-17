using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TableForGto.Controllers;
using TableForGto.Models;
using TableForGto.Models.Columns;
using TableForGto.Models.ColumnTypes;
using TableForGto.ViewModels;
using Windows.Media.AppBroadcasting;

namespace TableForGto.Serializers
{
    public class TableSerializer
    {
        private const string Column = "Column";

        private const string Student = "Student";

		private const string Title = "Title";

        private const string DateColumn = "DateColumn";

        private const string NumericColumn = "NumericColumn";

        private const string TextColumn = "TextColumn";

        private const string TimeColumn = "TimeColumn";

		const char PropertyDelimiter = '=';

		const char ValueDelimiter = '•';

		public static void Serialize(IEnumerable<Table> tables, ProjectInfo fileInfo)
        {
            if (!(fileInfo.Directory is DirectoryInfo directoryInfo && 
                directoryInfo.Exists
			))
            {
                throw new Exception($"Не существует: {fileInfo.FullName}");
            }

			var lines = new List<string>();

			foreach(var table in tables)
			{
				lines.Add($"{Title}={table.Title}");

				foreach (var column in table.MainColumns)
				{
					lines.Add(string.Join(PropertyDelimiter, new object[]
					{
						Column,
						string.Join(ValueDelimiter, new object[]
						{
							column.Header,
							column.DisplayIndex,
							column.Width
						})
					}));
				}

				foreach (var column in table.ResultColumns)
				{
					lines.Add(string.Join(PropertyDelimiter, new object[]
					{
						Column,
						string.Join(ValueDelimiter, new object[]
						{
							column.Header,
							column.DisplayIndex,
							column.Width,
							column.Type.GetType().Name
						})
					}));
				}
				
				foreach (var student in table.Students)
				{
					lines.Add(string.Join(PropertyDelimiter, new string[]
					{
						Student,
						string.Join(ValueDelimiter, student.DivideIntoCells())
					}));
				}
			}

			File.WriteAllLines(fileInfo.FullName, lines);
		}

        public static IEnumerable<Table> Deserialize(ProjectInfo fileInfo)
        {
			if (!(fileInfo.Directory is DirectoryInfo directoryInfo &&
				directoryInfo.Exists)
			)
			{
				throw new Exception($"Не существует: {fileInfo.FullName}");
			}

			var tables = new List<Table>();
			var lines = File.ReadAllLines(fileInfo.FullName);
			string[] splittedLine, values;
			string property, value;
			
			foreach (var line in lines)
			{
				splittedLine = line.Split(PropertyDelimiter, 2);
				(property, value) = (splittedLine[0], splittedLine[1]);
				switch (property)
				{
					case Title:
						tables.Add(new Table() 
						{ 
							Title = value,
							MainColumns = new List<MainColumn>(),
							ResultColumns = new List<ResultColumn>(),
							Students = new List<Student>()
						});
						break;

					case Column:
						values = value.Split(ValueDelimiter);

						if(tables.Last().MainColumns.Count < DataGridModelView.MainColumnsAmount)
						{
							tables.Last().MainColumns.Add(new MainColumn
							{
								Header = values[0],
								DisplayIndex = int.Parse(values[1]),
								Width = double.Parse(values[2])
							});
						}
						else
						{
							tables.Last().ResultColumns.Add(new ResultColumn
							{
								Header = values[0],
								DisplayIndex = int.Parse(values[1]),
								Width = double.Parse(values[2]),
								Type = values[3] switch
								{
									NumericColumn => new NumericColumn(),
									TimeColumn => new TimeColumn(),
									DateColumn => new DateColumn(),
									TextColumn => new TextColumn(),
									_ => throw new NotImplementedException()
								}
							});
						}
						break;

					case Student:
						values = value.Split(ValueDelimiter);
						var students = tables.Last().Students;

						students.Add(new Student
						{
							Rating = values[0] == string.Empty ?
									null :
									int.Parse(values[0]),
							Fio = values[1],
							Group = values[2],
							Results = new ObservableCollection<IColumnType?>(
									ParseResults(
										values.Take(Range.StartAt(Index.FromStart(3))),
										tables.Last().ResultColumns
								))
						});
						break;

					default:
						throw new NotImplementedException();
				}
			}

			foreach(var table in tables)
			{
				int i = 0;

				foreach(var column in table.MainColumns)
				{
					column.BindingPath = DataGridModelView
						.DefaultMainColumns[i]
						.BindingPath;
					column.BindingMode = DataGridModelView
						.DefaultMainColumns[i]
						.BindingMode;
					i++;
				}
			}

			return tables;

			static IEnumerable<IColumnType?> ParseResults(
				IEnumerable<string> values,
				ICollection<ResultColumn> resultColumns
			)
			{
				return values.Select((value, i) =>
				{
					if(value == string.Empty)
					{
						return null;
					}

					IColumnType? result = resultColumns
								.ElementAt(i)
								.Type
								.GetType()
								.Name switch
					{
						NumericColumn => Models.ColumnTypes.NumericColumn.Parse(value),
						TimeColumn => Models.ColumnTypes.TimeColumn.Parse(value),
						DateColumn => Models.ColumnTypes.DateColumn.Parse(value),
						TextColumn => new TextColumn(value),
						_ => throw new NotImplementedException()
					};

					return result;
				});
			}
		}
    }
}
