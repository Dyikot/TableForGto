using System.Windows.Input;


namespace TableForGto.Commands
{
	public static class TableCommands
	{
		public static readonly RoutedUICommand AddNumericColumn =
			new RoutedUICommand(
				text: "С числовый форматом",
				name: "AddNumericColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand AddTextColumn =
			new RoutedUICommand(
				text: "С текстовым форматом",
				name: "AddTextColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand AddTimeColumn =
			new RoutedUICommand(
				text: "С форматом времени",
				name: "AddTimeColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand AddDateColumn =
			new RoutedUICommand(
				text: "С форматом даты",
				name: "AddDateColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand RenameColumn =
			new RoutedUICommand(
				text: "Переименовать",
				name: "RenameColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand RemoveColumn =
			new RoutedUICommand(
				text: "Удалить",
				name: "RemoveColumn",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand MakeRating =
			new RoutedUICommand(
				text: "Составить",
				name: "MakeRating",
				ownerType: typeof(TableCommands),
				inputGestures: new InputGestureCollection()
				{
					new KeyGesture(Key.F5)
				}
		);

		public static readonly RoutedUICommand DeleteSelection =
			new RoutedUICommand(
				text: "Значения",
				name: "DeleteSelection",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand DeleteRows =
			new RoutedUICommand(
				text: "Удалить",
				name: "DeleteRows",
				ownerType: typeof(TableCommands)
		);

		public static readonly RoutedUICommand CopyWithHeaders =
			new RoutedUICommand(
				text: "Копировать с заголовками",
				name: "CopyWithHeaders",
				ownerType: typeof(TableCommands),
				inputGestures: new InputGestureCollection()
				{
					new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Shift)
				}
		);

		public static readonly RoutedUICommand ClearRating =
			new RoutedUICommand(
				text: "Очистить",
				name: "ClearRating",
				ownerType: typeof(TableCommands),
				inputGestures: new InputGestureCollection()
				{
					new KeyGesture(Key.F5, ModifierKeys.Control)
				}
		);
	}
}
