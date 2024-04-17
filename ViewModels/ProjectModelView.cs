using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TableForGto.Models;

namespace TableForGto.ViewModels
{
	public class ProjectModelView
	{
		private ProjectInfo? _projectInfo;
		
		private readonly TextBox _nameView;

		private readonly TextBox _pathView;

		public ProjectModelView(TextBox projectName, TextBox projectPath)
		{
			_nameView = projectName;
			_pathView = projectPath;
		}

		public ProjectInfo? ProjectInfo
		{
			get => _projectInfo;
			set
			{
				_projectInfo = value;

				_nameView.Text = Path.GetFileNameWithoutExtension(_projectInfo.Name);
				_pathView.Text = _projectInfo.DirectoryName;
			}
		}
	}
}
