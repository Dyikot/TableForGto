using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TableForGto.Converters;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
    public partial class TableViewModel : ObservableObject
    {
		private readonly AddColumnDialogService _addColumnDialog = new();
		private int _columnId = 0;

		public TableViewModel(string title)
		{
			Columns = new List<ColumnViewModel>
			{
				new()
				{
					Name = "Place",
					Title = "Место",
					Order = 0,
					Width = 75,
					IsReadOnly = true,
				},
				new()
				{
					Name = "FullName",
					Title = "ФИО",
					Order = 1,
					Width = 250
				},
				new()
				{
					Name = "Group",
					Title = "Группа",
					Order = 2,
					Width = 125
				}
			};

			Title = title;
			Students = new(Enumerable.Range(0, 10).Select(i => new Student()));
		}

		public event EventHandler<ColumnViewModel>? ColumnAdded;
		public event EventHandler<ColumnViewModel>? ColumnRemoved;

		public string Title { get; set; }
        public ObservableCollection<Student> Students { get; set; }
		public List<ColumnViewModel> Columns { get; private set; }

		[RelayCommand]
		private void AddColumn()
		{
			if(_addColumnDialog.Show(out var result))
			{
				AddColumn(result.Title, result.Format);
			}
		}

		private void AddColumn(string title, ColumnFormat format)
		{
			var columnId = _columnId++;
			var name = columnId.ToString();

			object defaultValue = null!;
			IValueConverter? converter = null;

			switch (format)
			{
				case ColumnFormat.Int:
					defaultValue = 0;
					converter = new IntConverter();
					break;

				case ColumnFormat.Float:
					defaultValue = 0.0f;
					converter = new FloatConverter();
					break;

				case ColumnFormat.String:
					defaultValue = string.Empty;
					break;

				case ColumnFormat.Time:
					defaultValue = TimeOnly.MinValue;
					converter = new TimeConverter();
					break;

				case ColumnFormat.Date:
					defaultValue = DateOnly.MinValue;
					converter = new DateConverter();
					break;
			}

			var column = new ResultColumnViewModel(RemoveColumn)
			{
				Name = name,
				Title = title,
				Order = Columns.Count,
				DefaultValue = defaultValue,
				Converter = converter
			};

			Columns.Add(column);

			foreach(var student in Students)
			{
				student.Results[name] = defaultValue;
			}

			ColumnAdded?.Invoke(this, column);
		}

		public void RemoveColumn(ColumnViewModel column)
		{
			Columns.Remove(column);
			ColumnRemoved?.Invoke(this, column);
		}

		[RelayCommand]
		public void AddStudents(int count)
		{
			var columns = Columns.OfType<ResultColumnViewModel>().ToList();

			foreach (var i in Enumerable.Range(0, count))
			{
				var student = new Student();
				foreach (var column in columns)
				{
					student.Results.Add(column.Name, column.DefaultValue);
				}

				Students.Add(student);
			}
		}

		[RelayCommand]
		private async Task SetRatingAsync()
		{
			
		}

		[RelayCommand]
		private void ClearRating()
		{

		}
	}
}
