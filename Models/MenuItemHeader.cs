using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models
{
	public class MenuItemHeader : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public string DefaultValue { get; private set; }

		public string Value
		{
			get => _value;
			set
			{
				_value = value;
				NotifyPropertyChanged();
			}
		}

		private string _value;

		public MenuItemHeader(string defaultValue)
		{
			DefaultValue = defaultValue;
			_value = defaultValue;
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
