namespace SnappyMap.Generation.Quantization
{
    using System;
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;

    /// <summary>
    /// This quantizer samples the image at an offset.
    /// What this means is that instead of fitting a grid to the image exactly,
    /// the quantizer enlarges the grid so that half a cell on each edge
    /// falls outside the bounds of the image.
    /// 
    /// When sampling, the quantizer uses the nearest edge pixel
    /// for pixels outside the image bounds.
    /// 
    /// This is useful for labeling based on intersections,
    /// since labelling the inner intersections will cover all of the map,
    /// rather than missing the edges.
    /// </summary>
    public class OffsetMapQuantizer : IMapQuantizer
    {
        public OffsetMapQuantizer(int outputWidth, int outputHeight)
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

        private static int SampleArea(Bitmap image, Rectangle area)
        {
            int accum = 0;

            for (int y = 0; y < area.Height; y++)
            {
                for (int x = 0; x < area.Width; x++)
                {
                    var compX = Util.Clamp(area.X + x, 0, image.Width - 1);
                    var compY = Util.Clamp(area.Y + y, 0, image.Height - 1);
                    accum += image.GetPixel(compX, compY).R;
                }
            }

            return accum / (area.Width * area.Height);
        }

        private TerrainType SampleTerrain(Bitmap image, int x, int y)
        {
            return ValueToType(this.SampleCoordinates(image, x, y));
        }

        private int SampleCoordinates(Bitmap image, int x, int y)
        {
            int cellWidth = image.Width / (this.OutputWidth - 1);
            int cellHeight = image.Height / (this.OutputHeight - 1);

            int widthRem = image.Width % (this.OutputWidth - 1);
            int heightRem = image.Height % (this.OutputHeight - 1);

            int extraOffsetX = Math.Min(widthRem, x);
            int extraOffsetY = Math.Min(heightRem, y);

            int startX = (cellWidth * x) + extraOffsetX - (cellWidth / 2);
            int startY = (cellHeight * y) + extraOffsetY - (cellHeight / 2);

            if (x < widthRem)
            {
                cellWidth++;
            }

            if (y < heightRem)
            {
                cellHeight++;
            }

            return SampleArea(image, new Rectangle(startX, startY, cellWidth, cellHeight));
        }
    }
}