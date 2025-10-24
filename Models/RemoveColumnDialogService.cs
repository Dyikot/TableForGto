using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TableForGto.ViewModels.Dialogs;
using TableForGto.Views;

namespace TableForGto.Models
{
	public class RemoveColumnDialogService
	{
		public bool Show()
		{
			var dialog = new MessageDialog(
				"Удаление столбца", 
				"Вы действительно хотите удалить?", 
				MessageDialogIcon.Warning, 
				MessageDialogButtons.YesNot);
			return dialog.ShowDialog() ?? false;
		}
	}
}
