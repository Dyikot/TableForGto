using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows.Controls;
using System.Xml.Serialization;
using TableForGto.Collections;
using TableForGto.Controllers;
using TableForGto.Models;
using TableForGto.Serializers;

namespace TableForGto.ViewModels
{
	public class ProjectListModelView
	{
		public ProjectCollection Items { get; set; }

		private readonly ListBox _view;

		public ProjectListModelView(ListBox projectList)
		{
			Items = new ProjectCollection(ProjectSerializer.Deserialize());
			_view = projectList;
			_view.ItemsSource = Items;
		}
	}
}
