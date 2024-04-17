using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using TableForGto.Commands;
using TableForGto.Controllers;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
	public class TableMenuModelView
	{
		public event EventHandler CurrentItemChanged;

		public ToolBar View { get; set; }

		public TableTitleModelView TableTitleMV { get; set; }

		private readonly List<Button> _buttons = new();

		private readonly ObservableCollection<TableMenuItem> _items;

		private Button? _currentButton;

		public TableMenuModelView(
			ToolBar tableMenu, 
			TableTitleModelView tableTitleVM,
			IEnumerable<Table>? tables = null
		)
		{
			TableTitleMV = tableTitleVM;
			View = tableMenu;
			_items = new ObservableCollection<TableMenuItem>();
			View.ItemsSource = _items;

			View.Items.CurrentChanged += OnCurrentChanged;

			if(tables != null)
			{
				foreach(var tableTitle in tables)
				{
					AddMenuItem(tableTitle.Title);
				}
			}
		}

		public TableMenuItem? CurrentItem
		{
			get => (TableMenuItem)View.Items.CurrentItem;
			set
			{
				if (value != null)
				{
					View.Items.MoveCurrentTo(value);
				}
			}
		}

		public ReadOnlyCollection<TableMenuItem> Items => new(_items);

		public int CurrentPosition => _items.IndexOf(CurrentItem);

		public Button? CurrentButton
		{
			get => _currentButton;
			set
			{
				if(_currentButton == value)
				{
					return;
				}

				if (_currentButton != null)
				{
					_currentButton.Background = new SolidColorBrush(Colors.White);
				}

				_currentButton = value;

				if (_currentButton != null)
				{
					_currentButton.Background = new SolidColorBrush(Colors.LightSkyBlue);
				}
			}
		}

		public Button? GetButtonByItem(TableMenuItem menuItem)
		{
			return _buttons.Find(button => ((TableMenuItem)button.DataContext) == menuItem);
		}

		public void AddButton(Button button)
		{
			_buttons.Add(button);
			CurrentItem ??= (TableMenuItem)button.DataContext;
			CurrentButton ??= GetButtonByItem(CurrentItem);
		}

		public void AddMenuItem(string? name = null)
		{ 
			_items.Add(CreateMenuItem(name));
		}

		public void RemoveMenuItem(TableMenuItem menuItem)
		{
			var itemIndex = _items.IndexOf(menuItem);

			if (menuItem == CurrentItem)
			{
				if (itemIndex != 0)
				{
					View.Items.MoveCurrentToPrevious();
				}
				else
				{
					View.Items.MoveCurrentToNext();
				}
			}

			_items.RemoveAt(itemIndex);
			_buttons.RemoveAt(itemIndex);
		}

		public void MoveItemToOtherItemPlace(
			TableMenuItem sourceItem, 
			TableMenuItem targetItem
		)
		{
			var sourceIndex = _items.IndexOf(sourceItem);
			var targetIndex = _items.IndexOf(targetItem);

			_items.Move(sourceIndex, targetIndex);
			_buttons.Clear();
			View.Items.Refresh();
		}

		private void OnTableNameChanged(string name)
		{
			if(CurrentItem.Name == name)
			{
				TableTitleMV.Title = name;
				CurrentButton.Content = CurrentItem.ViewName;
			}
		}

		private TableMenuItem CreateMenuItem(string? name)
		{
			name ??= $"Таблица {Items.Count + 1}";
			var tableMenuItem = new TableMenuItem { Name = name };
			tableMenuItem.TableNameChanged += OnTableNameChanged;

			return tableMenuItem;
		}

		private void OnCurrentChanged(object? sender, EventArgs e)
		{
			CurrentButton = _buttons[CurrentPosition];
			CurrentItemChanged?.Invoke(sender, e);
		}
	}
}
