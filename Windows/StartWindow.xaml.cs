using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Win32;
using TableForGto.ViewModels;
using System.Windows.Input;
using TableForGto.Serializers;
using System.Collections.ObjectModel;
using TableForGto.Models;
using System.Linq;

namespace TableForGto.Windows
{
	public enum OpenMode { Default, NewProject }

	public partial class StartWindow : Window
	{
		public ProjectListModelView ProjectListMV { get; set; }

		public ProjectModelView ProjectMV { get; set; }

		private readonly OpenMode _openMode;

		public StartWindow(OpenMode openMode = OpenMode.Default)
		{
			InitializeComponent();

			_openMode = openMode;
			ProjectListMV = new ProjectListModelView(_recentProjectList);
			ProjectMV = new ProjectModelView(_projectName, _projectPath);

			if (_openMode is OpenMode.NewProject)
			{
				_tabControl.SelectedItem = _createNewProjectMenu;
				_backButton.Visibility = Visibility.Collapsed;
			}
		}

		private void OnCreateNewProjectButtonClick(object sender, RoutedEventArgs e)
		{
			_tabControl.SelectedItem = _createNewProjectMenu;
		}

		private void OnProjectDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var projectInfo = (ProjectInfo)((ListBoxItem)sender).Content;

			if (projectInfo.Exists)
			{
				Application.Current.MainWindow = new MainWindow(projectInfo);
				Application.Current.MainWindow.Show();
				Close();
			}	
			else
			{
				if(MessageBox.Show(
						$"Файл \"{projectInfo.NameOnly}\" не найден. Удалить из списка проектов?",
						"Ошибка при отрытии проекта",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question) == MessageBoxResult.Yes
				)
				{
					ProjectListMV.Items.Remove(projectInfo);
				}
			}
		}

		private void OnProjectNameTextBoxLoaded(object sender, RoutedEventArgs e)
		{
			if (_projectName.Focus())
			{
				_projectName.SelectAll();
			}
		}

		private void OnProjectLocationButtonClick(object sender, RoutedEventArgs e)
		{
			var saveFileDialog = new SaveFileDialog()
			{
				InitialDirectory = Environment.CurrentDirectory,
				FileName = _projectName.Text,
				Filter = "Расширение проекта (*.tgto)|*.tgto",
				Title = "Создание проекта"
			};

			if (saveFileDialog.ShowDialog().HasValue == true)
			{
				ProjectMV.ProjectInfo = new ProjectInfo(saveFileDialog.FileName);
			}
		}

		private void OnBackButtonClick(object sender, RoutedEventArgs e)
		{
			_tabControl.SelectedItem = _startMenu;
		}

		private void OnProjectPathInitialized(object sender, EventArgs e)
		{
			_projectPath.Text = Environment.CurrentDirectory;
		}

		private void OnOpenProjectButtonClick(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog()
			{
				InitialDirectory = Environment.CurrentDirectory,
				Filter = "Расширение проекта (*.tgto)|*.tgto",
				Title = "Открытие проекта"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				var projectInfo = new ProjectInfo(openFileDialog.FileName);

				ProjectListMV.Items.Add(projectInfo);
				Application.Current.MainWindow = new MainWindow(projectInfo);
				Application.Current.MainWindow.Show();
				Close();
			}
		}

		private void OnWindowClosed(object sender, EventArgs e)
		{
			ProjectSerializer.Serialize(ProjectListMV.Items);
		}

		private void CreateNewProjectCommandExecuted(
			object sender, 
			ExecutedRoutedEventArgs e
		)
		{
			if (ProjectMV.ProjectInfo == null)
			{
				ProjectMV.ProjectInfo = new ProjectInfo(
					$"{_projectPath.Text}\\{_projectName.Text}.tgto"
				);
			}

			if (!ProjectMV.ProjectInfo.Directory.Exists)
			{
				_projectPath.BorderBrush = new SolidColorBrush(Colors.Red);
				return;
			}

			if (ProjectMV.ProjectInfo.Exists && MessageBox.Show(
					"Проект с таким именем уже существует. Заменить на новый?",
					"Создание проекта",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question) == MessageBoxResult.No)
			{
				return;
			}

			File.Create(ProjectMV.ProjectInfo.FullName).Close();
			ProjectListMV.Items.Add(ProjectMV.ProjectInfo);
			Application.Current.MainWindow = new MainWindow(ProjectMV.ProjectInfo);
			Application.Current.MainWindow.Show();

			if(_openMode == OpenMode.NewProject)
			{
				DialogResult = true;
			}

			Close();
		}
	}
}
