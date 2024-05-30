using System.Diagnostics;
using System.Windows;
using Wpf.Ui.Controls;

namespace FollowManager
{
	/// <summary>
	/// Logica di interazione per ResultsWindow.xaml
	/// </summary>
	public partial class ResultsWindow : FluentWindow
	{
		public ResultsWindow(List<Tuple<string, string>> listUnfollowers)
		{
			InitializeComponent();
			Owner = Application.Current.MainWindow;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			Unfollowers.ItemsSource = listUnfollowers;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
		private void Unfollowers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (Unfollowers.SelectedItem is Tuple<string, string> selectedItem)
			{
				string url = selectedItem.Item2;
				Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
			}
		}
	}
}
