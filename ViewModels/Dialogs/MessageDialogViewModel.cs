using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TableForGto.ViewModels.Dialogs
{
	public enum MessageDialogIcon
	{
		Info, Warning, Error
	}

	public enum MessageDialogButtons
	{
		Ok, OkCancel, YesNot
	}

	public class MessageDialogViewModel : DialogViewModel
	{
		public MessageDialogViewModel(string title,
									  string description,
									  MessageDialogIcon icon,
									  MessageDialogButtons buttons)
		{
			Title = title;
			Description = description;

			var fileName = icon switch
			{
				MessageDialogIcon.Error => "Error.png",
				MessageDialogIcon.Warning => "Warning.png",
				MessageDialogIcon.Info => "Info.png",
				_ => throw new NotSupportedException()
			};

			var uri = new Uri($"/Assets/Images/MessageDialog/{fileName}", UriKind.Relative);
			Icon = new BitmapImage(uri);

			switch (buttons)
			{
				case MessageDialogButtons.Ok:
					SubmitText = "ОК";
					CancelText = "";
					CancelVisibility = Visibility.Collapsed;
					break;

				case MessageDialogButtons.OkCancel:
					SubmitText = "ОК";
					CancelText = "Отмена";
					break;

				case MessageDialogButtons.YesNot:
					SubmitText = "Да";
					CancelText = "Нет";
					break;

				default:
					throw new NotSupportedException();
			}
		}

		public string Title { get; init; }
		public string Description { get; init; }
		public BitmapImage Icon { get; init; }
		public string SubmitText { get; init; }
		public string CancelText { get; set; }
		public Visibility CancelVisibility { get; init; } = Visibility.Visible;
	}
}
