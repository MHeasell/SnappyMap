namespace SnappyMap.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SnappyMap.Data;

    [Serializable]
    public struct SectionMapping
    {
        public SectionMapping(SectionType type, params string[] sections)
            : this()
        {
            this.Type = type;
            this.Sections = sections.ToList();
        }

        public SectionType Type { get; set; }

        public List<string> Sections { get; set; }
    }

    [Serializable]
    public class SectionConfig
    {
        public SectionConfig()
        {
            this.SectionMappings = new List<SectionMapping>();
        }

        public List<SectionMapping> SectionMappings { get; private set; }
    }
}
