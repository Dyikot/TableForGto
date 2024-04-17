using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TableForGto.ViewModels
{
	public class TableTitleModelView
	{
		private readonly TextBox _tableTitle;

		public string Title
		{
			get => _tableTitle.Text;
			set 
			{
				var width = value.Length * _tableTitle.FontSize * 0.6;
				var min = Math.Min(width, _tableTitle.ActualWidth);

				_tableTitle.MinWidth = min == 0 ? width : min;
				_tableTitle.Text = value;
			}
		}

		public bool IsReadOnly => _tableTitle.IsReadOnly;

		public TableTitleModelView(TextBox tableTitle)
		{
			_tableTitle = tableTitle;
		}

		public void MakeEditable()
		{
			_tableTitle.SelectAll();
			_tableTitle.IsReadOnly = false;
			_tableTitle.BorderThickness = new System.Windows.Thickness(1);
		}

		public void MakeReadOnly()
		{
			_tableTitle.IsReadOnly = true;
			_tableTitle.BorderThickness = new System.Windows.Thickness(0);
			_tableTitle.Select(0, 0);
		}
	}
}
