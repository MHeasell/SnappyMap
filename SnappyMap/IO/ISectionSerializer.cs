namespace SnappyMap.IO
{
    using System.IO;

    using SnappyMap.Data;

    public interface ISectionSerializer
    {
        void WriteSection(Stream output, Section data);
    }
}
