using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
    public partial class AddColumnModel : ObservableValidator
    {
        [ObservableProperty]
		[Required(ErrorMessage = "Поле не может быть пустым")]
		private string _title = string.Empty;

        public event EventHandler? Submitted;

        public int FormatIndex { get; set; } = 0;
        public ColumnFormat Format => (ColumnFormat)FormatIndex;

        [RelayCommand]
		private void Submit()
        {
            ValidateAllProperties();
            
            if (!HasErrors)
            {
                Submitted?.Invoke(this, EventArgs.Empty);
            }
        }
	}
}
