namespace SnappyMap.Rendering
{
    using SnappyMap.Collections;
    using SnappyMap.Data;

    public interface ISectionGridRenderer
    {
        Section Render(IGrid<Section> sectionGrid);
    }
}
