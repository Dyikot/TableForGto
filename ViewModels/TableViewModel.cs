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
		private int _columnId = 0;
		private readonly List<ColumnViewModel> _columns;

		public TableViewModel(string title)
        {
            Title = title;            
            Students = [];

			_columns = new List<ColumnViewModel>
			{
				new MainColumnViewModel
				{
					Header = new("Place", "Место"),
					Order = 0,
					Width = 75, 
					IsReadOnly = true, 
				},
				new MainColumnViewModel
				{
					Header = new("FullName", "ФИО"),
					Order = 1,
					Width = 250
				},
				new MainColumnViewModel
				{
					Header = new("Group", "Группа"),
					Order = 2,
					Width = 125
				}
			};
        }

        public string Title { get; set; }
        public ObservableCollection<Student> Students { get; set; }
		public IEnumerable<ColumnViewModel> Columns => _columns;

		public void AddColumn(string title, ColumnFormat format)
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

			_columns.Add(new ResultColumnViewModel
			{
				Header = new(name, title),
				Order = _columns.Count,
				DefaultValue = defaultValue,
				Converter = converter
			});

			foreach(var student in Students)
			{
				student.Results[name] = defaultValue;
			}
		}

		public void RemoveColumn(string title)
		{

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
