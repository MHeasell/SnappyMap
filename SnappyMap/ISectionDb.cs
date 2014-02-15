namespace SnappyMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISectionDb
    {
        void RegisterSection(Section section, SectionType type);
    }
}
