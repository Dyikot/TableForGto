using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
    public partial class TableViewModel : ObservableObject
    {
        public TableViewModel(string title)
        {
            Title = title;
            Students = [];
        }

        public string Title { get; set; }
        public List<Student> Students { get; set; }

		public async Task SetRatingAsync()
		{
            
		}

		public void ClearRating()
		{

		}
	}
}
