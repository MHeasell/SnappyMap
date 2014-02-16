namespace SnappyMap.Data
{
    using SnappyMap.Collections;

    public class Section
    {
        public Section(int width, int height)
        {
            this.TileData = new Grid<Tile>(width, height);

            this.HeightData = new Grid<int>(width * 2, height * 2);
        }

        public IGrid<int> HeightData { get; private set; }

        public IGrid<Tile> TileData { get; set; }
    }
}
