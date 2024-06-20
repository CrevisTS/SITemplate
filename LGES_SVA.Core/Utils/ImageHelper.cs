using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LGES_SVA.Core.Utils
{
	public class ImageHelper
	{
		/// <summary>
		/// Byte배열을 Bitmap으로 변환합니다.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static Bitmap ByteToBitmap(byte[] data, int width, int height)
		{
			// 여기에서 높이, 너비 및 형식을 알고있는 비트 맵을 만듭니다. 
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

			// BitmapData 생성 및 기록 될 모든 픽셀을 GC가 수집할 수 없도록 잠금 
			BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

			// 포인트 사용한다고 정의 하며 마샬 메모리 등록
			IntPtr unmanagedPointer = Marshal.AllocHGlobal(data.Length);

			// 바이트 배열의 데이터를 BitmapData로 복사합니다.
			Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
			// 마샬 메모리 해제
			Marshal.FreeHGlobal(unmanagedPointer);
			// 픽셀 잠금 해제 
			bmp.UnlockBits(bmpData);

			return bmp;
		}

		/// <summary>
		/// Bitmap을 BitmapImage로 변환합니다.
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
		{
			// 새 비트맵 이미지 객체 생성
			BitmapImage bitmapimage = new BitmapImage();
			// 메모리 스트림을 사용한다.
			using (MemoryStream stream = new MemoryStream())
			{
				
				bitmap.Save(stream, ImageFormat.Bmp);					// 메모리 스트림에 bitmap을 Bmp로 저장한다.
				stream.Position = 0;									// 스트림 포지션 0으로 설정해 처음부터 잡는다.
				bitmapimage.BeginInit();								// 비트맵 이미지 초기화
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;     // 비트맵 캐시옵셥 : 이미지가 다 생성되야 stream 닫게설정
				bitmapimage.StreamSource = stream;						// 스트림소스에 스트림 내용을 집어 넣는다.
				bitmapimage.EndInit();									// 비트맵 이미지 초기화 종료
				bitmapimage.Freeze();									// 이미지 변경을 더 이상 하지 않는다고 선언(바인딩 권한 위해 필수)
				bitmap.Dispose();										// 필요 없으면 Dispose해

				return bitmapimage;
			}
		}

		/// <summary>
		/// Gray 이미지를 만들기 위해 사용합니다.
		/// </summary>
		/// <param name="bitmap"></param>
		public static void SetGrayscalePalette(Bitmap bitmap)
		{
			ColorPalette GrayscalePalette = bitmap.Palette;

			if (GrayscalePalette.Entries.Length != 256)
			{
				var monoBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
				GrayscalePalette = monoBmp.Palette;

				for (int i = 0; i < GrayscalePalette.Entries.Length; i++)
				{
					GrayscalePalette.Entries[i] = Color.FromArgb(i, i, i);
				}
				bitmap.Palette = GrayscalePalette;
				return;
			}

			for (int i = 0; i < GrayscalePalette.Entries.Length; i++)
			{
				GrayscalePalette.Entries[i] = Color.FromArgb(i, i, i);
			}
			bitmap.Palette = GrayscalePalette;
		}

		/// <summary>
		/// Bitmap을 WriteableBitmap으로 변환합니다.
		/// </summary>
		/// <param name="writeBmp"></param>
		/// <returns></returns>
		public static Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
		{
			System.Drawing.Bitmap bmp;
			using (MemoryStream outStream = new MemoryStream())
			{
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
				enc.Save(outStream);
				bmp = new System.Drawing.Bitmap(outStream);
			}
			return bmp;
		}
	}
}
