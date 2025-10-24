using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TableForGto.Converters;
using TableForGto.ViewModels;

namespace TableForGto.DataTemplates
{
	public class ColumnTemplate : ITemplate<ColumnViewModel, DataGridTextColumn>
	{
		private readonly Image _renameIcon = new();
		private readonly Image _removeIcon = new();

		public ColumnTemplate()
		{
			var renameUri = new Uri("/Assets/Images/Edit.png", UriKind.Relative);
			var removeUri = new Uri("/Assets/Images/Close.png", UriKind.Relative);

			_renameIcon.Source = new BitmapImage(renameUri);
			_removeIcon.Source = new BitmapImage(removeUri);
			_renameIcon.Width = _renameIcon.Height = 24;			
			_removeIcon.Width = _removeIcon.Height = 24;
		}

		public DataGridTextColumn Build(ColumnViewModel param)
		{
			var header = new DataGridColumnHeader();

			if(param is ResultColumnViewModel resultColumn)
			{
				var rename = new MenuItem
				{
					Icon = _renameIcon,
					Header = "Переименовать",
					Command = resultColumn.RenameCommand
				};

				var remove = new MenuItem
				{
					Icon = _removeIcon,
					Header = "Удалить",
					Command = resultColumn.RemoveCommand
				};

				var contextMenu = new ContextMenu();
				contextMenu.Items.Add(rename);
				contextMenu.Items.Add(remove);

				header.ContextMenu = contextMenu;
			}

			var column = new DataGridTextColumn
			{
				Header = header,
				Binding = param.NewBinding(),
				IsReadOnly = param.IsReadOnly
			};

			var titleBinding = new Binding("Title")
			{
				Source = param,
				Mode = BindingMode.OneWay,
				FallbackValue = param.Title
			};

			var widthBinding = new Binding("Width")
			{
				Source = param,
				Mode = BindingMode.OneWayToSource,
				Converter = new ColumnWidthConverter(),
				FallbackValue = param.Width
			};

			var indexBinding = new Binding("Order")
			{
				Source = param,
				Mode = BindingMode.OneWayToSource,
				FallbackValue = param.Order
			};			

			BindingOperations.SetBinding(header, ContentControl.ContentProperty, titleBinding);
			BindingOperations.SetBinding(column, DataGridColumn.WidthProperty, widthBinding);
			BindingOperations.SetBinding(column, DataGridColumn.DisplayIndexProperty, indexBinding);

			return column;
		}
	}
}
