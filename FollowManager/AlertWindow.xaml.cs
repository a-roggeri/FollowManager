using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FollowManager
{
	/// <summary>
	/// Logica di interazione per AlertWindow.xaml
	/// </summary>
	public partial class AlertWindow : FluentWindow
	{
		public AlertWindow()
		{
			InitializeComponent();
			Owner = Application.Current.MainWindow;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
    }
}
