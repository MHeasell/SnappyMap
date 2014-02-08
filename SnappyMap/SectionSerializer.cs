namespace SnappyMap
{
    using System.IO;

    using SnappyMap.IO;

    using TAUtil.Tnt;

    public class SectionSerializer : ISectionSerializer
    {
        public void WriteSection(Stream output, Section data)
        {
            TntWriter writer = new TntWriter(output);

            writer.WriteTnt(new TntAdapter(data));
        }
    }
}