namespace SnappyMap
{
    using SnappyMap.Collections;

    public interface ISectionGridRenderer
    {
        Section Render(IGrid<Section> sectionGrid);
    }
}
