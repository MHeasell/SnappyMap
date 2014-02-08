namespace SnappyMap
{
    using SnappyMap.Collections;

    public interface ISectionTypeRealizer
    {
        IGrid<Section> Realise(IGrid<SectionType> types);
    }
}
