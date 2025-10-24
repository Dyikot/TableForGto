using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using TableForGto.Converters;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
	public class ColumnViewModel : ObservableObject
	{
		public required string Name { get; init; }
		public required string Title { get; set; }
		public required int Order { get; set; }
		public double Width { get; set; } = 150;
		public bool IsReadOnly { get; set; }

		public virtual Binding NewBinding() => new Binding(Name);
	}

	public partial class ResultColumnViewModel : ColumnViewModel
	{
		private readonly Action<ColumnViewModel> _removeColumn;
		private readonly RenameColumnDialogService _renameDialog = new();
		private readonly RemoveColumnDialogService _removeDialog = new();

		public ResultColumnViewModel(Action<ColumnViewModel> removeColumn)
		{
			_removeColumn = removeColumn;
		}

		public required object DefaultValue { get; init; }
		public required IValueConverter? Converter { get; init; }
		public override Binding NewBinding() => new Binding($"Results[{Name}]")
		{
			Converter = Converter
		};

		[RelayCommand]
		private void Rename()
		{
			if (_renameDialog.Show(out var title))
			{
				Title = title;
				OnPropertyChanged(nameof(Title));
			}
		}

		[RelayCommand]
		private void Remove()
		{
			if (_removeDialog.Show())
			{
				_removeColumn(this);
			}
		}
	}
}
