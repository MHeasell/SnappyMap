namespace SnappyMap
{
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;

    using SnappyMap.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            string inputPath = args[0];
            string outputPath = args[1];
            string searchPath = args[2];

            Bitmap image = new Bitmap(inputPath);

            ITerrainCreator creator = CreateTerrainCreator(searchPath);

            Section terrain = creator.CreateTerrainFrom(image);

            ISectionSerializer serializer = CreateSectionSerializer();

            using (Stream output = File.Create(outputPath))
            {
                serializer.WriteSection(output, terrain);
            }
        }

        private static ITerrainCreator CreateTerrainCreator(string searchPath)
        {
            var tileDatabase = new TileDatabase(SHA1.Create());

            var sectionDatabaseFactory = new SectionDatabaseFactory(new SectionLoader(tileDatabase));
            var sectionDatabase = sectionDatabaseFactory.CreateDatabaseFrom(searchPath);

            var sectionDecider = new SectionDecider(
                new SectionTypeLabeler(),
                new SectionTypeRealizer(sectionDatabase));

            return new TerrainCreator(
                new MapQuantizer(16, 16),
                sectionDecider,
                new SectionGridRenderer());
        }

        private static ISectionSerializer CreateSectionSerializer()
        {
            return new SectionSerializer();
        }
    }
}
