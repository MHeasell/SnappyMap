namespace SnappyMap.Generation
{
    using System;
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;

    public class FuzzyRealizer : ISectionRealizer
    {
        private readonly IIndexedSectionSelector selector;

        private readonly Bitmap image;

        private int cellWidth;

        private int widthRem;

        private int cellHeight;

        private int heightRem;

        public FuzzyRealizer(IIndexedSectionSelector selector, Bitmap image)
        {
            this.selector = selector;
            this.image = image;
        }

        public IGrid<Section> Realise(IGrid<SectionType> types)
        {
            this.cellWidth = this.image.Width / types.Width;
            this.widthRem = this.image.Width % types.Width;
            this.cellHeight = this.image.Height / types.Height;
            this.heightRem = this.image.Height % types.Height;

            return types.MapFromIndexed(this.Map);
        }

        private Section Map(int x, int y, SectionType type)
        {
            int extraOffsetX = Math.Min(this.widthRem, x);
            int extraOffsetY = Math.Min(this.heightRem, y);

            int startX = (this.cellWidth * x) + extraOffsetX;
            int startY = (this.cellHeight * y) + extraOffsetY;

            int width = this.cellWidth;
            int height = this.cellHeight;

            if (x < this.widthRem)
            {
                width++;
            }

            if (y < this.heightRem)
            {
                height++;
            }

            Rectangle rect = new Rectangle(startX, startY, width, height);
            return this.selector.ChooseSection(type, this.image, rect);
        }
    }
}