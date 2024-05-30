using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FollowManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : FluentWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Followers_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Followers.Text.Trim() != string.Empty || Followed.Text.Trim() != string.Empty)
			{
				Compare.IsEnabled = true;
			}
			else
			{
				Compare.IsEnabled = false;
			}
		}

		private void Followed_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Followers.Text.Trim() != string.Empty || Followed.Text.Trim() != string.Empty)
			{
				Compare.IsEnabled = true;
			}
			else
			{
				Compare.IsEnabled = false;
			}
		}

		private void Compare_Click(object sender, RoutedEventArgs e)
		{
			List<string> listFollowers = new List<string>();
			List<string> listFollowed = new List<string>();
			List<string> listUnfollowers = new List<string>();
			
			if(Followers.Text.Trim() != string.Empty)
			{
				try
				{
					var jsonFollowers = JArray.Parse(Followers.Text.Trim());
					foreach (var list in jsonFollowers)
					{
						listFollowers.Add(list["string_list_data"][0]["value"].ToString());
					}

				}
				catch (JsonReaderException jex)
				{
					System.Windows.MessageBox.Show("Errore di parsing: " + jex.Message, "Attenzione", System.Windows.MessageBoxButton.OK);
					return;
				}
				catch (Exception ex) //some other exception
				{
					System.Windows.MessageBox.Show("Errore generico: " + ex.Message, "Attenzione", System.Windows.MessageBoxButton.OK);
					return;
				}
			}
			if (Followed.Text.Trim() != string.Empty)
			{
				try
				{
					var jsonFollowedContainer = JObject.Parse(Followed.Text.Trim());
					var jsonFollowed = jsonFollowedContainer.SelectToken("relationships_following").ToList<JToken>();
					foreach (var list in jsonFollowed)
					{
						listFollowed.Add(list["string_list_data"][0]["value"].ToString());
					}
				}
				catch (JsonReaderException jex)
				{
					System.Windows.MessageBox.Show("Errore di parsing: " + jex.Message, "Attenzione", System.Windows.MessageBoxButton.OK);
					return;
				}
				catch (Exception ex) //some other exception
				{
					System.Windows.MessageBox.Show("Errore generico: " + ex.Message, "Attenzione", System.Windows.MessageBoxButton.OK);
					return;
				}
			}
			listUnfollowers.AddRange(listFollowed);
			listUnfollowers.RemoveAll(item => listFollowers.Contains(item));
			ResultsWindow results = new ResultsWindow(this, listUnfollowers);
			this.Hide();
			results.Show();
		}

		private void OpenFileFollowers_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "File zip (*.zip)|*.zip|Tutti i file (*.*)|*.*";

			openFileDialog.ShowDialog();
				string filePath = openFileDialog.FileName;
			try
			{
				using (ZipArchive archive = ZipFile.Open(filePath, ZipArchiveMode.Read))
				{
					ZipArchiveEntry entry = archive.GetEntry("connections/followers_and_following/followers_1.json");
					using (StreamReader reader = new StreamReader(entry.Open()))
					{
						string line = string.Empty;
						while ((line = reader.ReadLine()) != null)
						{
							Followers.Text += line;
						}
					}
				}
			}

			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
				return;
			}
			
		}

		private void OpenFileFollowed_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "File json (following.json)|following.json|Tutti i file (*.*)|*.*";

			openFileDialog.ShowDialog();
			string filePath = openFileDialog.FileName;
			try
			{
				string fileContent = File.ReadAllText(filePath);
				Followed.Text = fileContent;
			}

			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
				return;
			}
		}

		private void Clear_Click(object sender, RoutedEventArgs e)
		{
			Followers.Clear();
			Followed.Clear();
		}

		private void OpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = "File zip (*.zip)|*.zip|Tutti i file (*.*)|*.*";

			openFileDialog.ShowDialog();
			string filePath = openFileDialog.FileName;
			try
			{
				ZipFile.ExtractToDirectory(filePath, System.IO.Path.GetDirectoryName(filePath) + "\\extraction", true);
				string followersContent = File.ReadAllText(System.IO.Path.GetDirectoryName(filePath) + "\\extraction\\connections\\followers_and_following\\followers_1.json");
				Followers.Text = followersContent;
				string followedContent = File.ReadAllText(System.IO.Path.GetDirectoryName(filePath) + "\\extraction\\connections\\followers_and_following\\following.json");
				Followed.Text = followedContent;
			}

			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
				return;
			}
		}
	}
}