namespace SnappyMap.IO
{
    using System.IO;

    using SnappyMap.Data;

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