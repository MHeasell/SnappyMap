namespace SnappyMap.Generation
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public class SectionRealizer : ISectionRealizer
    {
        private readonly ISectionChooser chooser;

        public SectionRealizer(ISectionChooser chooser)
        {
            this.chooser = chooser;
        }

        public IGrid<Section> Realise(IGrid<SectionType> types)
        {
            return types.MapFrom(x => this.chooser.ChooseSectionOfType(x));
        }
    }
}
