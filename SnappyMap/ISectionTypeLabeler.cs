namespace SnappyMap
{
    using SnappyMap.Collections;

    public interface ISectionTypeLabeler
    {
        IGrid<SectionType> LabelIntersections(IGrid<TerrainType> layout);
    }
}
