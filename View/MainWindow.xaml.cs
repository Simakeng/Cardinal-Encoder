using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using 音声无损压缩器.Model;
using 音声无损压缩器.ViewModel;

namespace 音声无损压缩器
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindowLoaded(object sender, RoutedEventArgs e)
		{

		}

		private void btnOnStartEncoding(object sender, RoutedEventArgs e)
		{
			var arr = lbFileItem.ItemsSource as ArrayList;
			return;
		}

		private void ctmOnShowLogs(object sender, RoutedEventArgs e)
		{
			if (!Common.ConsoleManager.HasConsole)
				Common.ConsoleManager.Show();
			return;
		}

		private void ctmOnViewFilePath(object sender, RoutedEventArgs e)
		{
			var file = lbFileItem.SelectedItem as AudioFileInfo;
			if (file == null)
				return;

			Process.Start("explorer.exe", "/select," + file.filePath);
		}
	}
}
