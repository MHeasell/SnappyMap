namespace SnappyMapTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SnappyMap;
    using SnappyMap.Collections;

    [TestClass]
    public class SectionGridRendererTests
    {
        [TestMethod]
        public void TestRender()
        {
            var renderer = new SectionGridRenderer();

            var tile1 = new Tile(new byte[] { 0 });
            var sect1 = new Section(1, 1);
            sect1.TileData[0] = tile1;
            sect1.HeightData[0] = 0;
            sect1.HeightData[1] = 1;
            sect1.HeightData[2] = 2;
            sect1.HeightData[3] = 3;

            var input = new Grid<Section>(2, 2);
            input[0] = sect1;
            input[1] = sect1;
            input[2] = sect1;
            input[3] = sect1;

            var expected = new Section(2, 2);
            expected.TileData[0] = tile1;
            expected.TileData[1] = tile1;
            expected.TileData[2] = tile1;
            expected.TileData[3] = tile1;
            expected.HeightData[0] = 0;
            expected.HeightData[1] = 1;
            expected.HeightData[2] = 0;
            expected.HeightData[3] = 1;
            expected.HeightData[4] = 2;
            expected.HeightData[5] = 3;
            expected.HeightData[6] = 2;
            expected.HeightData[7] = 3;
            expected.HeightData[8] = 0;
            expected.HeightData[9] = 1;
            expected.HeightData[10] = 0;
            expected.HeightData[11] = 1;
            expected.HeightData[12] = 2;
            expected.HeightData[13] = 3;
            expected.HeightData[14] = 2;
            expected.HeightData[15] = 3;

            var output = renderer.Render(input);

            Assert.IsTrue(GridMethods.GridEquals(expected.TileData, output.TileData));
            Assert.IsTrue(GridMethods.GridEquals(expected.HeightData, output.HeightData));
        }
    }
}
