namespace SnappyMap
{
    using System.Drawing;

    using SnappyMap.Collections;

    public class FuzzyRealizer : ISectionTypeRealizer
    {
        private readonly IIndexedSectionSelector selector;

        private readonly int sctWidth;

        private readonly int sctWidthRem;

        private readonly int sctHeight;

        private readonly int sctHeightRem;

        private readonly Bitmap image;

        public FuzzyRealizer(IIndexedSectionSelector selector, int mapWidth, int mapHeight, Bitmap image)
        {
            this.selector = selector;
            this.sctWidth = image.Width / mapWidth;
            this.sctWidthRem = image.Width % mapWidth;
            this.sctHeight = image.Height / mapHeight;
            this.sctHeightRem = image.Height % mapHeight;
            this.image = image;
        }

        public IGrid<Section> Realise(IGrid<SectionType> types)
        {
            return types.MapFromIndexed(this.Map);
        }

        private Section Map(int x, int y, SectionType type)
        {
            // add 1 to x and y here because only interior intersections are considered.
            x += 1;
            y += 1;

            Rectangle rect = new Rectangle(
                (this.sctWidth * x) - (this.sctWidth / 2),
                (this.sctHeight * y) - (this.sctHeight / 2),
                this.sctWidth,
                this.sctHeight);

            return this.selector.ChooseSection(type, this.image, rect);
        }
    }
}