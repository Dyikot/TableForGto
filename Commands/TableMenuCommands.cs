using System.Windows.Input;

namespace TableForGto.Commands
{
	public static class TableMenuCommands
	{
		public static readonly RoutedUICommand Remove =
			new RoutedUICommand(
				text: "Удалить",
				name: "Remove",
				ownerType: typeof(TableMenuCommands)
		);

		public static readonly RoutedUICommand Rename =
			new RoutedUICommand(
				text: "Переименовать",
				name: "Rename",
				ownerType: typeof(TableMenuCommands)
		);

		public static readonly RoutedUICommand Copy =
			new RoutedUICommand(
				text: "Создать копию",
				name: "Copy",
				ownerType: typeof(TableMenuCommands)
		);

		
	}
}
