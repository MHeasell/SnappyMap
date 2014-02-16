namespace SnappyMap.IO
{
    using System.IO;

    using SnappyMap.Data;

    using TAUtil.Hpi;
    using TAUtil.Tnt;

    public class SectionHpiSerializer
    {
        public void WriteSection(string filename, Section data)
        {
            string tntFilename = Path.GetTempFileName();

            string otaFilename = Path.GetTempFileName();

            try
            {
                string namePart = Path.GetFileNameWithoutExtension(filename);

                using (var f = File.Create(tntFilename))
                {
                    TntWriter writer = new TntWriter(f);
                    writer.WriteTnt(new TntAdapter(data));
                }

                MapAttributes attrs = new MapAttributes();
                using (var f = File.Create(otaFilename))
                {
                    attrs.WriteOta(f);
                }

                using (HpiWriter w = new HpiWriter(filename, HpiWriter.CompressionMethod.ZLib))
                {
                    w.AddFile("maps\\" + namePart + ".tnt", tntFilename);
                    w.AddFile("maps\\" + namePart + ".ota", otaFilename);
                }
            }
            finally
            {
                if (File.Exists(tntFilename))
                {
                    File.Delete(tntFilename);
                }

                if (File.Exists(otaFilename))
                {
                    File.Delete(otaFilename);
                }
            }
        }
    }
}
