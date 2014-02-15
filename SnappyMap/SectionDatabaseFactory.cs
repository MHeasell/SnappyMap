namespace SnappyMap
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    using SnappyMap.IO;

    using TAUtil.Hpi;

    public class SectionDatabaseFactory
    {
        private readonly SectionLoader loader;

        public SectionDatabaseFactory(SectionLoader loader)
        {
            this.loader = loader;
        }

        public ISectionDatabase CreateDatabaseFrom(string path, SectionConfig config)
        {
            SectionDatabase db = new SectionDatabase();

            this.PopulateDatabase(db, path, config);

            return db;
        }

        public IIndexedSectionSelector CreateFuzzyDatabaseFrom(string path, SectionConfig config)
        {
            var db = new IndexedSectionSelector();

            this.PopulateDatabase(db, path, config);

            return db;
        }

        private static Dictionary<string, SectionType> GetTypeMapping(SectionConfig config)
        {
            Dictionary<string, SectionType> types = new Dictionary<string, SectionType>();

            foreach (var mapping in config.SectionMappings)
            {
                foreach (var filename in mapping.Sections)
                {
                    types[filename.Replace("/", @"\")] = mapping.Type;
                }
            }

            return types;
        }

        private void PopulateDatabase(ISectionDb db, string path, SectionConfig config)
        {
            var types = GetTypeMapping(config);

            HashSet<string> hpiExtensions = new HashSet<string> { ".hpi", ".ufo", ".ccx", ".gpf", ".gp3" };

            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(file);
                if (ext != null && hpiExtensions.Contains(ext))
                {
                    this.LoadFromHpi(file, db, types);
                }
                else
                {
                    string relPath = file.Substring(path.Length + 1);
                    SectionType type;
                    if (types.TryGetValue(relPath, out type))
                    {
                        Section sect = this.loader.ReadSection(file);
                        db.RegisterSection(sect, type);
                    }
                }
            }
        }

        private void LoadFromHpi(string hpiFile, ISectionDb db, Dictionary<string, SectionType> types)
        {
            using (HpiReader reader = new HpiReader(hpiFile))
            {
                foreach (var file in reader.GetFilesRecursive(string.Empty))
                {
                    SectionType type;
                    if (types.TryGetValue(file.Name, out type))
                    {
                        using (var s = reader.ReadFile(file.Name))
                        {
                            Section sect = this.loader.ReadSection(s);
                            db.RegisterSection(sect, type);
                        }
                    }
                }
            }
        }
    }
}
