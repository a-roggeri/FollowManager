using Newtonsoft.Json.Linq;
using System.Linq;
using System.Windows;
using Wpf.Ui.Controls;

namespace FollowManager
{
	/// <summary>
	/// Logica di interazione per ResultsWindow.xaml
	/// </summary>
	public partial class ResultsWindow : FluentWindow
	{
		MainWindow main;
		List<string> listUnfollowers = new List<string>();
		public ResultsWindow(MainWindow main, List<string> listUnfollowersParameter)
		{
			InitializeComponent();
			this.main = main;
			listUnfollowers = listUnfollowersParameter;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
			main.Show();
		}

		private void Unfollowers_Loaded(object sender, RoutedEventArgs e)
		{
			foreach(var unfollower in listUnfollowers)
			{
				Unfollowers.Text = Unfollowers.Text + unfollower + "\n";
			}
		}
	}
}
