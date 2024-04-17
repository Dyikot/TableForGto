using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models.ColumnTypes;

namespace TableForGto.Models
{
    public class Student: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public int? Rating
		{
			get => _rating;
			set
			{
				_rating = value;
				NotifyPropertyChanged();
			}
		}

		public string? Fio
		{
			get => _fio;
			set
			{
				_fio = value;
				NotifyPropertyChanged();
			}
		}

		public string? Group
		{
			get => _group;
			set
			{
				_group = value;
				NotifyPropertyChanged();
			}
		}

		public ObservableCollection<IColumnType?> Results { get; set; } = new();

		private int? _rating;

		private string? _fio;

		private string? _group;

		public IEnumerable<string> DivideIntoCells()
		{
			string rating = _rating.ToString();
			string fio = _fio == null ? string.Empty : _fio;
			string group = _group == null ? string.Empty : _group;

			return new List<string> { rating, fio, group }
				.Concat(Results
					.Select(result => result == null ? string.Empty : result.ToString()));
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}	
}
