using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.ViewModels
{
	public partial class RenameColumnDialogModel : DialogViewModel
	{
		[ObservableProperty]
		[Required(ErrorMessage = "Поле не может быть пустым")]
		private string _title = string.Empty;
	}
}
