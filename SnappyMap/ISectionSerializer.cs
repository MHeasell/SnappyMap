namespace SnappyMap
{
    using System.IO;

    public interface ISectionSerializer
    {
        void WriteSection(Stream output, Section data);
    }
}
