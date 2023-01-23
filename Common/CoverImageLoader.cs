using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace 音声无损压缩器.Common
{
	public class CoverImageLoader
	{
		public static ImageSource defaultImageSource { get; } = new BitmapImage(new Uri(Common.Helper.Constants.PlaceHolderImgUri));

		/// <summary>
		/// 从二进制数据创建 ImageSource
		/// </summary>
		/// <param name="audioFilePath"></param>
		/// <param name="MIMEType"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static ImageSource CreateImageSourceFromBinaryData(string audioFilePath, string MIMEType, byte[] data)
		{
			ImageSource imageSource = defaultImageSource;

			using (MemoryStream ms = new MemoryStream(data, false))
			{
				BitmapDecoder decoder = null;
				if (MIMEType == "image/png")
					decoder = new PngBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				else if (MIMEType == "image/jpeg")
					decoder = new JpegBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				else if (MIMEType == "image/tiff")
					decoder = new TiffBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				else if (MIMEType == "image/bmp")
					decoder = new BmpBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				else if (MIMEType == "image/gif")
					decoder = new GifBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				else if (MIMEType == "image/vnd.microsoft.icon")
					decoder = new IconBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

				if (decoder != null)
					imageSource = decoder.Frames[0];
				else
					throw new Exception($"未知的 MIME 图片格式, 解码失败. ({MIMEType} : {audioFilePath})");
			}

			return imageSource;
		}
	}
}
