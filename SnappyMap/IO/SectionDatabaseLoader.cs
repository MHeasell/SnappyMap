namespace SnappyMap.IO
{
    using System.Collections.Generic;
    using System.IO;

    using SnappyMap.Data;
    using SnappyMap.Database;

    using TAUtil.Hpi;

    public class SectionDatabaseLoader
    {
        private readonly SectionLoader loader;

        public SectionDatabaseLoader(SectionLoader loader)
        {
            this.loader = loader;
        }

        public void PopulateDatabase(ISectionDatabase db, string path, SectionConfig config)
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

        private void LoadFromHpi(string hpiFile, ISectionDatabase db, Dictionary<string, SectionType> types)
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