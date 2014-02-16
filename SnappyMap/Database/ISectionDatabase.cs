namespace SnappyMap.Database
{
    using SnappyMap.Data;

    public interface ISectionDatabase
    {
        void RegisterSection(Section section, SectionType type);
    }
}
