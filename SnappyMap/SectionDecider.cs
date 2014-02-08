namespace SnappyMap
{
    using SnappyMap.Collections;

    public class SectionDecider : ISectionDecider
    {
        private readonly ISectionTypeLabeler labeler;

        private readonly ISectionTypeRealizer realizer;

        public SectionDecider(ISectionTypeLabeler labeler, ISectionTypeRealizer realizer)
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