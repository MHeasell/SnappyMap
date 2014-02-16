namespace SnappyMap.Generation.Quantization
{
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;

    public interface IMapQuantizer
    {
        /// <summary>
        /// Processes the given image into a boolean grid
        /// representing land/sea areas of the map.
        /// </summary>
        IGrid<TerrainType> QuantizeImage(Bitmap image);
    }
}
