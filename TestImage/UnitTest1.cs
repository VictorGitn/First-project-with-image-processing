using NUnit.Framework;
using Recognizer;

namespace TestImage
{
    [TestFixture]
    public class Grayscale_Tests
    {
        [TestCase(0, 0, 0, 0)]
        [TestCase(255, 255, 255, 1)]
        [TestCase(255, 128, 0, 0.59365)]
        public void Test_Image1x1(byte r, byte g, byte b, double value)
        {
            var expected = new double[1, 1] { { value } };
            var original = new Pixel[1, 1] { { new Pixel(r, g, b) } };
            var actual = GrayscaleTask.ToGrayscale(original);
            Assert.AreEqual(true, (actual.GetLength(0) == 1) &&
                        (actual.GetLength(1) == 1), "size wrong");
            Assert.AreEqual(expected[0, 0], actual[0, 0], 1e-5);
        }

        [Test]
        public void Test_Image3x5()
        {
            var expected = new double[3, 5] {
            { 0.50991, 0.22592, 0.36746, 0.66561, 0.47825 },
            { 0.64256, 0.58040, 0.50116, 0.59859, 0.44619 },
            { 0.20372, 0.62305, 0.76864, 0.59618, 0.35273 } };

            var original = new Pixel[3, 5] {
            { new Pixel(164, 97, 211), new Pixel(174, 0, 49), new Pixel(234, 35, 28), new Pixel(134, 204, 87), new Pixel(234, 54, 178) },
            { new Pixel(35, 218, 223), new Pixel(255, 97, 130), new Pixel(89, 165, 38), new Pixel(135, 187, 22), new Pixel(116, 86, 251) },
            { new Pixel(99, 7, 160), new Pixel(240, 122, 136), new Pixel(245, 166, 222), new Pixel(38, 214, 132), new Pixel(184, 43, 85) } };

            var actual = GrayscaleTask.ToGrayscale(original);

            var isSizeEquval = (expected.GetLength(0) == actual.GetLength(0)) &&
                               (expected.GetLength(1) == actual.GetLength(1));

            Assert.AreEqual(true, isSizeEquval, "size wrong");
            for (int i = 0; i < expected.GetLength(0); i++)
                for (int j = 0; j < expected.GetLength(1); j++)
                    Assert.AreEqual(expected[i, j], actual[i, j], 1e-5, "wrong x = {0}, y = {1}", i, j);
        }
    }

    [TestFixture]
    public class MedianFillter_Tests
    {
        [Test]
        public void Test_Image1x1()
        {
            var expend = new double[,] { { 0.1 }};
            var pixels = new double[,] { { 0.1 }};
            var actual = MedianFilterTask.MedianFilter(pixels);
            Assert.AreEqual(expend, actual);
        }
    }

    [TestFixture]
    public class ThresholdFillter_Tests
    {
        [Test]
        public void Test_Image1x1()
        {
            var expend = new double[,] { { 0 } };
            var pixels = new double[,] { { 123 } };
            var actual = ThresholdFilterTask.ThresholdFilter(pixels, 0);
            Assert.AreEqual(expend, actual);
        }
    }
}