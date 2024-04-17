using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TableForGto.Windows;
using Windows.Globalization;

namespace TableForGto
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
		App()
		{
			InitializeComponent();
		}

		private void OnStartUp(object sender, StartupEventArgs e)
		{
			Current.MainWindow = new StartWindow();
			Current.MainWindow.Show();
        }
    }
}
