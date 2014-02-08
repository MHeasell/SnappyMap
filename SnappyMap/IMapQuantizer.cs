namespace SnappyMap
{
    using System.Drawing;

    using SnappyMap.Collections;

    public interface IMapQuantizer
    {
        /// <summary>
        /// Processes the given image into a boolean grid
        /// representing land/sea areas of the map.
        /// </summary>
        IGrid<TerrainType> QuantizeImage(Bitmap image);
    }
}
