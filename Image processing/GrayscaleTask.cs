namespace Recognizer
{
	public static class GrayscaleTask
	{
		/// <summary>
		/// Converts the image to gray using the formula: brightness = (0.299*R + 0.587*G + 0.114*B) / 255
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			int width = original.GetLength(0);
			int high = original.GetLength(1);
			double[,] grayPixels = new double[width, high];
			for (int x = 0; x < width; x++)
            {
				for (int y = 0; y < high; y++)
				{
					grayPixels[x, y] = (0.299 * original[x, y].R + 0.587 * original[x, y].G + 0.114 * original[x, y].B) / 255;
				}
			}
			return grayPixels;
		}
	}
}