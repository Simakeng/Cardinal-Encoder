using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace 音声无损压缩器.ViewModel
{
	public class AudioFileInfoViewModel : Model.AudioFileInfo
	{
		private MainWindowViewModel parent;
		public ICommand cmdOnDeleteFileInfo { get; private set; }
		public AudioFileInfoViewModel(MainWindowViewModel parent, string filePath) : base(filePath)
		{
			this.parent = parent;

			cmdOnDeleteFileInfo = new RelayCommand(() =>
			{
				Debug.WriteLine("called");
				this.parent.RemoveAudioFile(this);
			});
		}
	}
}
