using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using 音声无损压缩器.Common;
using GalaSoft.MvvmLight.CommandWpf;
using 音声无损压缩器.ViewModel;

namespace 音声无损压缩器.ViewModel
{
	public class MainWindowViewModel : IDropTarget, INotifyPropertyChanged
	{
		public MainWindowViewModel()
		{
			audioFileInfos = new ObservableCollection<AudioFileInfoViewModel>();
			_consoleLog = "准备就绪.";

			cmdOnDeleteFileInfo = new RelayCommand(() =>
			{
				Debug.WriteLine("test");
			});
		}

		private string _consoleLog;
		public string consoleLog
		{
			get
			{
				return _consoleLog;
			}
			set
			{
				_consoleLog = value;
				if (PropertyChanged != null)
					PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ConsoleLog"));
			}
		}

		public ICommand cmdOnDeleteFileInfo { get; private set; }

		public string RJID { get; set; }
		public ObservableCollection<AudioFileInfoViewModel> audioFileInfos
		{ get; } = new ObservableCollection<AudioFileInfoViewModel>();

		public event PropertyChangedEventHandler PropertyChanged;

		public void DragEnter(IDropInfo dropInfo)
		{
			return;
		}

		public void DragLeave(IDropInfo dropInfo)
		{
			return;
		}

		/// <summary>
		/// 响应鼠标拖拽悬浮事件
		/// </summary>
		/// <param name="dropInfo"></param>
		public void DragOver(IDropInfo dropInfo)
		{
			dropInfo.Effects = DragDropEffects.None;

			// 判断是否是顺序调整类型的拖放
			if (dropInfo.Data is Model.AudioFileInfo)
			{
				dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
				dropInfo.Effects = DragDropEffects.Move;
				return;
			}

			// 判断是否是插入文件类型的拖放
			var dataObject = dropInfo.Data as IDataObject;
			if (dataObject == null)
				return;
			if (!dataObject.GetDataPresent(DataFormats.FileDrop))
				return;
			dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
			dropInfo.Effects = DragDropEffects.Copy;
		}

		/// <summary>
		/// 实现拖放排序和文件拖入的逻辑
		/// </summary>
		/// <param name="dropInfo"></param>
		public void Drop(IDropInfo dropInfo)
		{
			int insertIndex = dropInfo.UnfilteredInsertIndex;
			if (dropInfo.Data is Model.AudioFileInfo)
			{
				int sourceIndex = dropInfo.DragInfo.SourceIndex;
				// 判断是否是顺序调整类型的拖放
				Debug.WriteLine($"dropInfo source:{sourceIndex} insert:{dropInfo.InsertIndex} unfilterd:{dropInfo.UnfilteredInsertIndex} relation:{dropInfo.InsertPosition}");
				if (sourceIndex <= insertIndex)
					insertIndex -= 1;
				audioFileInfos.Move(sourceIndex, insertIndex);
				return;
			}

			var dataObject = dropInfo.Data as DataObject;
			if (dataObject == null)
				return;
			if (!dataObject.ContainsFileDropList())
				return;

			string errorName = "";
			string successName = "";
			int errorCount = 0;
			int successCount = 0;
			var fileList = dataObject.GetFileDropList();
			foreach (var fileName in fileList)
			{
				var nameLower = fileName.ToLower();
				if (!(nameLower.EndsWith(".wav") ||
				nameLower.EndsWith(".wav") ||
				nameLower.EndsWith(".flac") ||
				nameLower.EndsWith(".mp3")
				))
				{
					errorName = fileName;
					consoleLog = $"拖放的文件 '{fileName}' 不是音频文件.";
					errorCount++;
					continue;
				}

				var info = new AudioFileInfoViewModel(this, fileName);
				successName = fileName;

				audioFileInfos.Insert(insertIndex++, info);
				successCount++;
			}

			if (errorCount == 0)
			{
				if (successCount == 1)
					consoleLog = $"成功导入文件 '{successName}'.";
				else
					consoleLog = $"成功导入了 {successCount} 个文件.";
			}
			else if (errorCount == 1)
			{
				if (successCount == 0)
					consoleLog = $"文件 '{errorName}' 导入时出现错误.";
				else
					consoleLog = $"成功导入了 {successCount} 个文件，'{errorName}' 文件导入时出现错误.";
			}
			else
			{
				if (successCount == 0)
					consoleLog = $"导入 {errorCount} 个文件时全部出现错误.";
				else
					consoleLog = $"拖入了 {errorCount + successCount} 个文件，其中 {successCount} 个文件成功导入，{errorCount} 个文件时出现错误.";

			}
		}

		public void RemoveAudioFile(AudioFileInfoViewModel info)
		{
			audioFileInfos.Remove(info);
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("audioFileInfos"));
		}

	}
}
