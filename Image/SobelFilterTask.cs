using System;

namespace Recognizer
{
    public static class SobelFilterTask
    {
        /// <summary>
        /// Applies a sobel filter to a set of pixels
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sx"></param>
        /// <returns></returns>
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sizeX = sx.GetLength(0);
            var sizeY = sx.GetLength(1);
            var deltaX = sizeX / 2;
            var deltaY = sizeY / 2;
            var sy = GetTransposeMatrix(sx);
            for (int x = deltaX; x < width - deltaX; x++)
            {
                for (int y = deltaY; y < height - deltaY; y++)
                {
                    var neighborhoodPoint = GetNeighborhoodPoint(g, x, y, sizeX);
                    var gx = MultiplyMatrix(neighborhoodPoint, sx);
                    var gy = MultiplyMatrix(neighborhoodPoint, sy);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }    
            return result;
        }
        /// <summary>
        /// Transposes the matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double[,] GetTransposeMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            var transposedMatrix = new double[m, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }
            return transposedMatrix;
        }
        /// <summary>
        /// Returns an array of pixel neighbors
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static double[,] GetNeighborhoodPoint(double[,] original, int x, int y, int size)
        {
            double[,] neighborhoodPoint = new double[size, size];
            neighborhoodPoint.Initialize();
            var delta = size / 2;
            for (int i = x - delta; i <= x + delta; i++)
            {
                for (int j = y - delta; j <= y + delta; j++)
                {
                        neighborhoodPoint[i - (x - delta), j - (y-delta)] = original[i, j]; 
                }
            }
            return neighborhoodPoint;
        }
        /// <summary>
        /// Multiplies matrices element by element
        /// </summary>
        /// <param name="neighborhood"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        static double MultiplyMatrix(double[,] neighborhood, double[,] s)
        {
            var r = 0d;
            var size = neighborhood.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    r += neighborhood[i, j] * s[i, j];
                }
            }
            return r;
        }
    }
}