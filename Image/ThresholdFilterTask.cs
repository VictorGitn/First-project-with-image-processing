using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		/// <summary>
		/// Makes the image black and white using threshold transformation
		/// </summary>
		/// <param name="original">array of pixels</param>
		/// <param name="whitePixelsFraction"></param>
		/// <returns>black and white array of pixels</returns>
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int countWhitePixels = (int)(original.Length * whitePixelsFraction);
			int width = original.GetLength(0);
			int high = original.GetLength(1);
			var withThresholdFilter = new double[width, high];
			double thresholdValue = GetThresholdValue(original, whitePixelsFraction);
			for (int x = 0; x < width; x++)
            {
				for (int  y= 0; y < high; y++)
				{
					if (original[x, y] >= thresholdValue && countWhitePixels != 0)
						withThresholdFilter[x, y] = 1.0;
					else
						withThresholdFilter[x,y] = 0.0;
				}
			}
			return withThresholdFilter;
		}

		/// <summary>
		/// Calculates the threshold value for a two-dimensional array of pixels
		/// </summary>
		/// <param name="original"></param>
		/// <param name="whitePixelsFraction"></param>
		/// <returns></returns>
		public static double GetThresholdValue(double[,] original, double whitePixelsFraction)
        {
			int countWhitePixels = (int)(original.Length * whitePixelsFraction);
			var listPixels = new List<double>(original.Length);
			int width = original.GetLength(0);
			int high = original.GetLength(1);
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < high; y++)
				{
					listPixels.Add(original[x, y]);
				}
			}
			var sortedListPixels = listPixels.OrderByDescending(n=>n).ToList();
			if(countWhitePixels == 0)
				return sortedListPixels[countWhitePixels];
			else
				return sortedListPixels[countWhitePixels-1];
		}
	}
}