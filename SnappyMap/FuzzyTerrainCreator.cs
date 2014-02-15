namespace SnappyMap
{
    using System.Drawing;

    using SnappyMap.Collections;

    public class FuzzyTerrainCreator : ITerrainCreator
    {
        private readonly IMapQuantizer quantizer;

        private readonly ISectionGridRenderer renderer;

        private readonly IIndexedSectionSelector selector;

        public FuzzyTerrainCreator(
            IMapQuantizer quantizer,
            IIndexedSectionSelector selector,
            ISectionGridRenderer renderer)
        {
            this.quantizer = quantizer;
            this.selector = selector;
            this.renderer = renderer;
        }

        public Section CreateTerrainFrom(Bitmap image)
        {
            IGrid<TerrainType> quantizedMap = this.quantizer.QuantizeImage(image);

            ISectionDecider decider = this.CreateDecider(image, quantizedMap.Width, quantizedMap.Height);
            IGrid<Section> sectionGrid = decider.DecideSections(quantizedMap);

            Section map = this.renderer.Render(sectionGrid);

            return map;
        }

        private ISectionDecider CreateDecider(Bitmap image, int width, int height)
        {
            return new SectionDecider(
                new SectionTypeLabeler(),
                new FuzzyRealizer(this.selector, width, height, image));
        }
    }
}
