namespace SnappyMap
{
    using SnappyMap.Collections;

    public class SectionGridRenderer : ISectionGridRenderer
    {
        public Section Render(IGrid<Section> sectionGrid)
        {
            int sectionWidth = sectionGrid[0].TileData.Width;
            int sectionHeight = sectionGrid[0].TileData.Height;

            var sct = new Section(sectionWidth * sectionGrid.Width, sectionHeight * sectionGrid.Height);

            for (int y = 0; y < sectionGrid.Height; y++)
            {
                for (int x = 0; x < sectionGrid.Width; x++)
                {
                    GridMethods.Copy(sectionGrid[x, y].TileData, sct.TileData, x, y);
                    GridMethods.Copy(sectionGrid[x, y].HeightData, sct.HeightData, x * 2, y  * 2);
                }
            }

            return sct;
        }
    }
}