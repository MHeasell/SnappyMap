namespace SnappyMap.Rendering
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

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
                    GridMethods.Copy(sectionGrid[x, y].TileData, sct.TileData, x * sectionWidth, y * sectionHeight);
                    GridMethods.Copy(sectionGrid[x, y].HeightData, sct.HeightData, x * sectionWidth * 2, y * sectionHeight * 2);
                }
            }

            return sct;
        }
    }
}