using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TableForGto.Models;
using TableForGto.Models.Columns;
using TableForGto.ViewModels;

namespace TableForGto.Controllers
{
    public class DataGridModelViewCollection: ObservableCollection<DataGridModelView>
	{
		public DataGridModelView Current { get; private set; }		

		public void Add(
			DataGrid dataGrid,
			IEnumerable<ResultColumn>? resultColumns = null,
			MainColumn[]? mainColumns = null,
			IEnumerable<Student>? students = null
		)
		{
			Add(new DataGridModelView(dataGrid, resultColumns, mainColumns, students));
			Current = Items.First();
		}

		public int CurrentPosition => Items.IndexOf(Current);

		public void MoveCurrentToPosition(int index)
		{
			Current.View.Visibility = System.Windows.Visibility.Collapsed;
			Current = Items[index];
			Current.View.Visibility = System.Windows.Visibility.Visible;
		}

		public void Remove(DataGrid dataGrid)
		{
			var tableMV = Items.First(tableMV => tableMV.View == dataGrid);
			int index = Items.IndexOf(tableMV);
			
			RemoveAt(index);
		}

		public void Duplicate(int dataGridIndex, DataGrid newDataGrid)
		{
			var resultColumns = Items[dataGridIndex].ResultColumns;
			var mainColumns = Items[dataGridIndex].MainColumns;
			var students = Items[dataGridIndex].Students;
			
			Add(newDataGrid, resultColumns, mainColumns, students);
		}
	}
}
