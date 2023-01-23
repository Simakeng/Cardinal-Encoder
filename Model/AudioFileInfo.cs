using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace 音声无损压缩器.Model
{
	public class AudioFileInfo
	{
		public string fileName { get; set; }
		public string filePath { get; set; }
		public object imgSource { get; set; }
		public string fileDesc { get; set; }

		public AudioFileInfo(string filePath)
		{
			this.filePath = filePath;

			fileName = Path.GetFileName(filePath);

			var tfile = TagLib.File.Create(filePath);


			var props = tfile.Properties;

			var codecs = props.Codecs.ToArray();

			fileDesc = $"采样率: {props.AudioSampleRate} 位深: {props.BitsPerSample}bit 比特率: {props.AudioBitrate}kbps 格式: {codecs[0].Description}";

			var pictures = tfile.Tag.Pictures;
			if (pictures.Length == 0)
				this.imgSource = new BitmapImage(new Uri(Common.Helper.Constants.PlaceHolderImgUri));
			else
			{
				foreach (var pic in pictures)
				{
					// 只提取封面图片
					if (pic.Type != TagLib.PictureType.FrontCover)
						continue;

					try
					{
						this.imgSource = Common.CoverImageLoader.CreateImageSourceFromBinaryData(filePath, pic.MimeType, pic.Data.Data);
					}
					catch
					{
						this.imgSource = Common.CoverImageLoader.defaultImageSource;
						throw;
					}

				}
			}

		}
	}
}
