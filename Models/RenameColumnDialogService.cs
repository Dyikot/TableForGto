using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.ViewModels;
using TableForGto.Views;

namespace TableForGto.Models
{
	public class RenameColumnDialogService
	{
		public bool Show(out string title)
		{
			var dialog = new RenameColumnDialog();
			if(dialog.ShowDialog() == true)
			{
				var vm = (RenameColumnDialogModel)dialog.DataContext;
				title = vm.Title;
				return true;
			}

			title = null!;
			return false;
		}
	}
}
