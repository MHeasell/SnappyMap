namespace SnappyMap.Generation
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public class SectionDecider : ISectionDecider
    {
        private readonly ISectionTypeLabeler labeler;

        private readonly ISectionRealizer realizer;

        public SectionDecider(ISectionTypeLabeler labeler, ISectionRealizer realizer)
        {
            this.labeler = labeler;
            this.realizer = realizer;
        }

        public IGrid<Section> DecideSections(IGrid<TerrainType> quantizedMap)
        {
            IGrid<SectionType> types = this.labeler.LabelIntersections(quantizedMap);

            return this.realizer.Realise(types);
        }
    }
}