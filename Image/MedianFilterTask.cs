using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		/// <summary>
		/// Median filter to get rid of noise
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static double[,] MedianFilter(double[,] original)
		{
			int width = original.GetLength(0);
			int high = original.GetLength(1);
			var noNoise = new double[width, high];
			for (int x = 0; x < width; x++)
            {
				for (int y = 0; y < high; y++)
				{
					noNoise[x, y] = GetMedian(original, width, high, x, y);
				}
			}
			return noNoise;
		}

		/// <summary>
		/// Calculates the median of pixels around
		/// </summary>
		/// <param name="original"></param>
		/// <param name="width"></param>
		/// <param name="high"></param>
		/// <param name="x">index x</param>
		/// <param name="y">index y</param>
		/// <returns></returns>
		public static double GetMedian(double[,] original,int width, int high,  int x, int y)
        {
			List<double> pixelsAround = new List<double>();
			for(int i = x-1; i <= x + 1; i++) 
			{
				for(int j = y-1; j <= y+1; j++)
                {
					if(i>=0 && i < width && j >= 0 && j < high)
						pixelsAround.Add(original[i, j]);
                }
			}
			pixelsAround.Sort();
			if (pixelsAround.Count() % 2 != 0) 
				return pixelsAround[pixelsAround.Count() / 2];
            else
				return (pixelsAround[(pixelsAround.Count() / 2) - 1] + pixelsAround[(pixelsAround.Count() / 2)]) / 2;	
        }
	}
}