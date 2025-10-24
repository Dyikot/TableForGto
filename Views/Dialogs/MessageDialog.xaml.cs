using System;
using System.Windows;
using TableForGto.ViewModels.Dialogs;

namespace TableForGto.Views
{
	public partial class MessageDialog : Window
	{
		public MessageDialog(string title, 
							 string description, 
							 MessageDialogIcon icon, 
							 MessageDialogButtons buttons)
		{
			InitializeComponent();
			var vm = new MessageDialogViewModel(title, description, icon, buttons);
			vm.Submitted += OnSubmitted;
			DataContext = vm;
		}

		private void OnSubmitted(object? sender, EventArgs e)
		{
			DialogResult = true;
		}
	}
}
