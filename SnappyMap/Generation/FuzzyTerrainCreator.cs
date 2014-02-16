namespace SnappyMap.Generation
{
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Generation.Quantization;

    public class FuzzyTerrainCreator : AbstractTerrainCreator
    {
        private readonly IMapQuantizer quantizer;

        private readonly IIndexedSectionSelector selector;

        public FuzzyTerrainCreator(
            IMapQuantizer quantizer,
            IIndexedSectionSelector selector)
        {
            this.quantizer = quantizer;
            this.selector = selector;
        }

        protected override IGrid<Section> CreateSectionsFrom(Bitmap image)
        {
            IGrid<TerrainType> quantizedMap = this.quantizer.QuantizeImage(image);

            ISectionDecider decider = this.CreateDecider(image, quantizedMap.Width, quantizedMap.Height);
            return decider.DecideSections(quantizedMap);
        }

        private ISectionDecider CreateDecider(Bitmap image, int width, int height)
        {
            return new SectionDecider(
                new SectionTypeLabeler(),
                new FuzzyRealizer(this.selector, width, height, image));
        }
    }
}
