namespace SnappyMap.Generation
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public interface ISectionTypeLabeler
    {
        IGrid<SectionType> LabelIntersections(IGrid<TerrainType> layout);
    }
}
