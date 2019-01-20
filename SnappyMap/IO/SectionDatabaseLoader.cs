namespace SnappyMap.IO
{
    using System.Collections.Generic;
    using System.IO;

    using SnappyMap.Data;
    using SnappyMap.Database;

    using TAUtil.HpiUtil;

    public class SectionDatabaseLoader
    {
        private static readonly HashSet<string> HpiExtensions = new HashSet<string>
            {
                ".hpi",
                ".ufo",
                ".ccx",
                ".gpf",
                ".gp3",
            };

        private readonly SectionLoader loader;

        private readonly string path;

        private readonly Dictionary<string, SectionType> typeMapping = new Dictionary<string, SectionType>();

        public SectionDatabaseLoader(SectionLoader loader, SectionConfig config, string path)
        {
            this.loader = loader;
            this.GetTypeMapping(config);
            this.path = path;
        }

        public void PopulateDatabase(ISectionDatabase db)
        {
            foreach (var file in Directory.EnumerateFiles(this.path, "*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(file);
                if (ext != null && HpiExtensions.Contains(ext))
                {
                    this.LoadFromHpi(file, db, this.typeMapping);
                }
                else
                {
                    string relPath = file.Substring(this.path.Length + 1);
                    SectionType type;
                    if (this.typeMapping.TryGetValue(relPath, out type))
                    {
                        Section sect = this.loader.ReadSection(file);
                        db.RegisterSection(sect, type);
                    }
                }
            }
        }

        private void GetTypeMapping(SectionConfig config)
        {
            foreach (var mapping in config.SectionMappings)
            {
                foreach (var filename in mapping.Sections)
                {
                    this.typeMapping[filename.Replace("/", @"\")] = mapping.Type;
                }
            }
        }

        private void LoadFromHpi(string hpiFile, ISectionDatabase db, Dictionary<string, SectionType> types)
        {
            using (var reader = new TAUtil.Hpi.HpiArchive(hpiFile))
            {
                foreach (var file in reader.GetFilesRecursive(string.Empty))
                {
                    SectionType type;
                    if (types.TryGetValue(file.Name, out type))
                    {
                        var buffer = new byte[file.Size];
                        reader.Extract(file, buffer);
                        var sect = this.loader.ReadSection(new MemoryStream(buffer));
                        db.RegisterSection(sect, type);
                    }
                }
            }
        }
    }
}