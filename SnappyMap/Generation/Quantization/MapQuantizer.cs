namespace SnappyMap.Generation.Quantization
{
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;

    public class MapQuantizer : IMapQuantizer
    {
        public MapQuantizer(int outputWidth, int outputHeight)
        {
            this.OutputWidth = outputWidth;
            this.OutputHeight = outputHeight;
        }

        public int OutputWidth { get; set; }

        public int OutputHeight { get; set; }

        public IGrid<TerrainType> QuantizeImage(Bitmap image)
        {
            var grid = new Grid<TerrainType>(this.OutputWidth, this.OutputHeight);

            for (int y = 0; y < this.OutputHeight; y++)
            {
                for (int x = 0; x < this.OutputWidth; x++)
                {
                    grid[x, y] = this.SampleTerrain(image, x, y);
                }
            }
            
            return grid;
        }

        private static TerrainType ValueToType(int value)
        {
            if (value < 128)
            {
                return TerrainType.Sea;
            }

            return TerrainType.Land;
        }

        private TerrainType SampleTerrain(Bitmap image, int x, int y)
        {
            return ValueToType(this.SampleCoordinates(image, x, y));
        }

        private int SampleCoordinates(Bitmap image, int x, int y)
        {
            int accum = 0;

            int cellWidth = image.Width / this.OutputWidth;
            int cellHeight = image.Height / this.OutputHeight;

            int startX = cellWidth * x;
            int startY = cellHeight * y;

            for (int dy = 0; dy < cellHeight; dy++)
            {
                for (int dx = 0; dx < cellWidth; dx++)
                {
                    accum += image.GetPixel(startX + dx, startY + dy).R;
                }
            }

            return accum / (cellWidth * cellHeight);
        }
    }
}