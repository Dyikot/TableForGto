using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TableForGto.Models
{
	public class TableMenuItem: INotifyPropertyChanged
	{
		private string _name = string.Empty;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
				TableNameChanged?.Invoke(value);
			}
		}

		public string ViewName { get { return $" {Name} "; } }

		public delegate void TableTitleChangeEventHandler(string title); 

		public event TableTitleChangeEventHandler? TableNameChanged;

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
