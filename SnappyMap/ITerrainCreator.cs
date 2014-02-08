namespace SnappyMap
{
    using System.Drawing;

    public interface ITerrainCreator
    {
        Section CreateTerrainFrom(Bitmap image);
    }
}
