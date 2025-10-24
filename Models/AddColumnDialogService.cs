using TableForGto.ViewModels;
using TableForGto.Views;

namespace TableForGto.Models
{
	public record AddColumnResult(string Title, ColumnFormat Format);

	public class AddColumnDialogService
	{
		public bool Show(out AddColumnResult result)
		{
			var dialog = new AddColumnDialog();
			if(dialog.ShowDialog() == true)
			{
				var vm = (AddColumnDialogModel)dialog.DataContext;
				result = new AddColumnResult(vm.Title, vm.Format);
				return true;
			}

			result = null!;
			return false;
		}
	}
}
