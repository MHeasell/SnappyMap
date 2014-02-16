namespace SnappyMap.Generation
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public interface ISectionRealizer
    {
        IGrid<Section> Realise(IGrid<SectionType> types);
    }
}
