using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
    public partial class TableViewModel : ObservableObject
    {
		private int _resultId = 0;

        public TableViewModel(string title)
        {
            Title = title;            
            Students = [];

			Columns = new List<ColumnViewModel>
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
        public List<ColumnViewModel> Columns { get; set; }

		public void AddColumn(object defaultValue, IValueConverter? converter = null)
		{
			var resultId = _resultId++;
			var name = resultId.ToString();
			var content = $"Столбец {Columns.Count + 1}";

			Columns.Add(new ResultColumnViewModel
			{
				Header = new(name, content),
				Order = Columns.Count,
				DefaultValue = defaultValue,
				Converter = converter
			});

			foreach(var student in Students)
			{
				student.Results[name] = defaultValue;
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
