using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.ViewModels
{
    public partial class DialogViewModel : ObservableValidator
	{
		public event EventHandler? Submitted;

		[RelayCommand]
		protected void Submit()
		{
			ValidateAllProperties();

			if (!HasErrors)
			{
				Submitted?.Invoke(this, EventArgs.Empty);
			}
		}
	}	
}
