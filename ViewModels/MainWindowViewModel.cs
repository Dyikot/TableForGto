using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TableForGto.Models;
using TableForGto.Views;

namespace TableForGto.ViewModels
{
	public partial class MainWindowViewModel : ObservableObject
	{
		[ObservableProperty]
		private TableViewModel _selectedTable;

		public ObservableCollection<TableViewModel> Tables { get; set; } = [];

		public MainWindowViewModel()
		{
			AddTable();
			_selectedTable = Tables.First();
		}

		[RelayCommand]
		private async Task SaveAsync()
		{
			
		}

		[RelayCommand]
		private void AddTable() => Tables.Add(CreateTable());

		private TableViewModel CreateTable() => new($"Таблица {Tables.Count + 1}");
	}
}
