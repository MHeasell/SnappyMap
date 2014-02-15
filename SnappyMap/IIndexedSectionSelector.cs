namespace SnappyMap
{
    using System.Drawing;

    public interface IIndexedSectionSelector
    {
        Section ChooseSection(SectionType type, Bitmap image, Rectangle imagePortion);
    }
}
