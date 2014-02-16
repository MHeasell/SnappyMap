namespace SnappyMap.Generation
{
    using System.Drawing;

    using SnappyMap.Data;

    public interface ITerrainCreator
    {
        Section CreateTerrainFrom(Bitmap image);
    }
}
