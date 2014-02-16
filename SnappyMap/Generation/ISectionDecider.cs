namespace SnappyMap.Generation
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public interface ISectionDecider
    {
        IGrid<Section> DecideSections(IGrid<TerrainType> quantizedMap);
    }
}
