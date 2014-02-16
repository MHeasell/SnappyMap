namespace SnappyMap
{
    using System.Drawing;

    using SnappyMap.Data;

    public interface IIndexedSectionSelector
    {
        Section ChooseSection(SectionType type, Bitmap image, Rectangle imagePortion);
    }
}
