namespace SnappyMap
{
    using System;
    using System.IO;

    using SnappyMap.IO;

    public class SectionDatabaseFactory
    {
        private readonly SectionLoader loader;

        public SectionDatabaseFactory(SectionLoader loader)
        {
            this.loader = loader;
        }

        public ISectionDatabase CreateDatabaseFrom(string path)
        {
            SectionDatabase db = new SectionDatabase();

            foreach (SectionType type in Enum.GetValues(typeof(SectionType)))
            {
                var searchPath = Path.Combine(path, type.ToString());

                foreach (var filePath in Directory.EnumerateFiles(searchPath))
                {
                    Section sect = this.loader.ReadSection(filePath);
                    db.RegisterSection(sect, type);
                }
            }

            return db;
        }
    }
}
