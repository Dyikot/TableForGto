using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TableForGto.Windows.IOWindows
{
	public partial class InputBox : Window
    {
		private readonly SolidColorBrush DefaultColor =
			new SolidColorBrush(Color.FromArgb(0xff, 0xab, 0xad, 0xb3));

		public InputBox(string messageBoxText, string caption, string value)
        {
            InitializeComponent();
            Title = caption;
            _message.Text = messageBoxText;
			_answer.Text = value;
			_answer.SelectAll();
		}

        public static string? Show(string messageBoxText, string caption, string value = "")
        {
			var inputBox = new InputBox(messageBoxText, caption, value);
			return inputBox.ShowDialog() is true ? inputBox._answer.Text : null;
		}

		private void OnOkButtonClick(object sender, RoutedEventArgs e)
		{
			if (IsAnswered())
			{
				DialogResult = true;
			}
		}

		private bool IsAnswered()
		{
			if (_answer.Text != string.Empty)
			{
				return true;
			}

			HighlightTextBox(_answer);
			return false;
		}

		private void HighlightTextBox(Control control)
		{
			control.BorderBrush = Brushes.Red;
			control.BorderThickness = new Thickness(2);
		}

		private void UnHighlightControl(Control control)
		{
			control.BorderBrush = DefaultColor;
			control.BorderThickness = new Thickness(1);
		}

		private void OnAnswerTextBoxGotFocus(object sender, RoutedEventArgs e)
		{
			UnHighlightControl(_answer);
		}
	}
}
