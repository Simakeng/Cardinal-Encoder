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


			//var arr = new ArrayList();
			//for (int i = 0; i < 10; i++) 
			//{
			//	var o = new DLSiteHelper.AudioFileInfo();
			//	o.fileName = "testFile " + i.ToString();
			//	o.fileDesc = "testADesc";
			//	o.imgUriPath = "Assets/place_holder.png";
			//	arr.Add(o);
			//}
			//lbFileItem.ItemsSource = arr;

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

		private void ctmOnDeleteFileInfo(object sender, RoutedEventArgs e)
		{
			var file = lbFileItem.SelectedItem as AudioFileInfoViewModel;
			if (file == null)
				return;
			var vm = this.DataContext as MainWindowViewModel;
			if (vm == null)
				return;
			vm.RemoveAudioFile(file);

		}

		private void ctmOnViewFilePath(object sender, RoutedEventArgs e)
		{
			var file = lbFileItem.SelectedItem as AudioFileInfo;
			if (file == null)
				return;

			Process.Start("explorer.exe", "/select," + file.filePath);
		}

		//private void FileDragEnter(object sender, DragEventArgs e)
		//{
		//	if (e.Data.GetDataPresent(DataFormats.FileDrop))
		//		e.Effects = DragDropEffects.Link;
		//	else
		//		e.Effects = DragDropEffects.None;
		//}

		//private void FileDragDrop(object sender, DragEventArgs e)
		//{
		//	var fileNames = (System.Array)e.Data.GetData(DataFormats.FileDrop);
		//	return;
		//}
	}
}
