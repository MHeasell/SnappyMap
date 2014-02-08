namespace SnappyMap
{
    using SnappyMap.Collections;

    public interface ISectionDecider
    {
        IGrid<Section> DecideSections(IGrid<TerrainType> quantizedMap);
    }
}
