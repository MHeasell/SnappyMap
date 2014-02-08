namespace SnappyMap
{
    using SnappyMap.Collections;

    public class SectionTypeRealizer : ISectionTypeRealizer
    {
        private readonly ISectionDatabase database;

        public SectionTypeRealizer(ISectionDatabase database)
        {
            this.database = database;
        }

        public IGrid<Section> Realise(IGrid<SectionType> types)
        {
            return types.MapFrom(x => this.database.ChooseSectionOfType(x));
        }
    }
}
