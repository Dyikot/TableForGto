using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TableForGto.Commands
{
	public static class WindowCommands
	{
		public static readonly RoutedUICommand Close =
			new RoutedUICommand(
				text: "Закрыть",
				name: "Close",
				ownerType: typeof(WindowCommands),
				inputGestures: new InputGestureCollection()
				{
					new KeyGesture(Key.F4, ModifierKeys.Alt)
				}
		);

		public static readonly RoutedUICommand CreateNewProject =
			new RoutedUICommand(
				text: "Создать",
				name: "CreateNewProject",
				ownerType: typeof(WindowCommands),
				inputGestures: new InputGestureCollection()
				{
					new KeyGesture(Key.Enter)
				}
		);
	}
}
