using System.Windows;
using System.Windows.Input;
using TableForGto.ViewModels;

namespace TableForGto.Views
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();

		}

		private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			
		}

		private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private void CloseExecuted(object sender, ExecutedRoutedEventArgs e) => Close();
	}
}