namespace SnappyMapTests
{
    using System.Drawing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SnappyMap;
    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Generation.Quantization;

    [TestClass]
    public class MapQuantizerTests
    {
        private const string CheckerFilePath = @"Images\checker.png";
        private const string WallFilePath = @"Images\wall.png";
        private const string DiagFilePath = @"Images\diag.png";
        private const string DotsFilePath = @"Images\dots.png";

        [TestMethod]
        public void SimpleChecker()
        {
            IGrid<TerrainType> expected = FromString(
                "SL",
                "LS");

            var q = new MapQuantizer(2, 2);

            Bitmap input = new Bitmap(CheckerFilePath);

            IGrid<TerrainType> output = q.QuantizeImage(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        [TestMethod]
        public void SimpleWall()
        {
            IGrid<TerrainType> expected = FromString(
                "SS",
                "LL");

            var q = new MapQuantizer(2, 2);

            Bitmap input = new Bitmap(WallFilePath);

            IGrid<TerrainType> output = q.QuantizeImage(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        [TestMethod]
        public void Diagonal()
        {
            IGrid<TerrainType> expected = FromString(
                "LSS",
                "SLS",
                "SSL");

            var q = new MapQuantizer(3, 3);

            Bitmap input = new Bitmap(DiagFilePath);

            IGrid<TerrainType> output = q.QuantizeImage(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        [TestMethod]
        public void Dots()
        {
            IGrid<TerrainType> expected = FromString(
                "SS",
                "SS");

            var q = new MapQuantizer(2, 2);

            Bitmap input = new Bitmap(DotsFilePath);

            IGrid<TerrainType> output = q.QuantizeImage(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        private static IGrid<TerrainType> FromString(params string[] lines)
        {
            return GridMethods.FromString(Util.CharToType, lines);
        }
    }
}
