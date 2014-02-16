namespace SnappyMap
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;
    using System.Xml.Serialization;

    using global::CommandLine;

    using SnappyMap.CommandLine;
    using SnappyMap.Data;
    using SnappyMap.Database;
    using SnappyMap.Generation;
    using SnappyMap.Generation.Quantization;
    using SnappyMap.IO;

    public class Program
    {
        private const int ErrorExitCode = 1;
        private const int SuccessExitCode = 0;

        public static int Main(string[] args)
        {
            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
            {
                return 1;
            }

            if (options.Items.Count < 1)
            {
                Console.WriteLine("Missing required arguments.");
                Console.WriteLine();
                Console.WriteLine(options.GetUsage());
                return ErrorExitCode;
            }

            string[] parts = options.Size.Split(new[] { 'x' }, 2);
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid map size: " + options.Size);
                Console.WriteLine();
                Console.WriteLine(options.GetUsage());
                return ErrorExitCode;
            }

            int mapWidth;
            int mapHeight;

            if (!int.TryParse(parts[0], out mapWidth))
            {
                Console.WriteLine("Invalid map width: " + parts[0]);
                Console.WriteLine();
                Console.WriteLine(options.GetUsage());
                return ErrorExitCode;
            }

            if (!int.TryParse(parts[1], out mapHeight))
            {
                Console.WriteLine("Invalid map height: " + parts[1]);
                Console.WriteLine();
                Console.WriteLine(options.GetUsage());
                return ErrorExitCode;
            }

            if (mapWidth <= 0 || mapHeight <= 0)
            {
                Console.WriteLine("Map dimensions ({0}x{1}) too small.", mapWidth, mapHeight);
                Console.WriteLine();
                Console.WriteLine(options.GetUsage());
                return ErrorExitCode;
            }

            string inputPath = options.Items[0];
            string outputPath = options.Items[1];
            string searchPath = options.LibraryPath;

            if (!File.Exists(options.ConfigFile))
            {
                Console.WriteLine("Config file not found: \"{0}\"", options.ConfigFile);
                return ErrorExitCode;
            }

            var configSerializer = new XmlSerializer(typeof(SectionConfig));
            SectionConfig config;
            try
            {
                using (Stream s = File.OpenRead(options.ConfigFile))
                {
                    config = (SectionConfig)configSerializer.Deserialize(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to read config file: " + e.Message);
                return ErrorExitCode;
            }

            ITerrainCreator creator;
            try
            {
                creator = CreateFuzzyTerrainCreator(searchPath, config, mapWidth, mapHeight);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading section library: " + e.Message);
                return ErrorExitCode;
            }

            ISectionSerializer serializer = new SectionSerializer();

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file '{0}' not found.", inputPath);
                return ErrorExitCode;
            }

            Bitmap image = new Bitmap(inputPath);

            Section terrain = creator.CreateTerrainFrom(image);

            try
            {
                using (Stream output = File.Create(outputPath))
                {
                    serializer.WriteSection(output, terrain);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing output: " + e.Message);
                return ErrorExitCode;
            }

            return SuccessExitCode;
        }

        private static ITerrainCreator CreateTerrainCreator(
            string tilesetDirectory,
            SectionConfig config,
            int mapWidth,
            int mapHeight)
        {
            var tileDatabase = new TileDatabase(SHA1.Create());

            var databaseLoader = new SectionDatabaseLoader(
                new SectionLoader(tileDatabase),
                config,
                tilesetDirectory);
            var sectionDatabase = new SectionDatabase();
            databaseLoader.PopulateDatabase(sectionDatabase);

            var sectionDecider = new SectionDecider(
                new SectionTypeLabeler(),
                new SectionRealizer(sectionDatabase));

            return new TerrainCreator(
                new OffsetMapQuantizer(mapWidth + 1, mapHeight + 1),
                sectionDecider);
        }

        private static ITerrainCreator CreateFuzzyTerrainCreator(
            string tilesetDirectory,
            SectionConfig config,
            int mapWidth,
            int mapHeight)
        {
            var tileDatabase = new TileDatabase(SHA1.Create());

            var databaseLoader = new SectionDatabaseLoader(
                new SectionLoader(tileDatabase),
                config,
                tilesetDirectory);
            var db = new IndexedSectionSelector();
            databaseLoader.PopulateDatabase(db);

            return new FuzzyTerrainCreator(
                new OffsetMapQuantizer(mapWidth + 1, mapHeight + 1),
                db);
        }
    }
}
