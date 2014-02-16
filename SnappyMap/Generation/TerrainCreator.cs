namespace SnappyMap.Generation
{
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Generation.Quantization;

    public class TerrainCreator : AbstractTerrainCreator
    {
        private readonly IMapQuantizer quantizer;

        private readonly ISectionDecider producer;

        public TerrainCreator(
            IMapQuantizer quantizer,
            ISectionDecider producer)
        {
            this.quantizer = quantizer;
            this.producer = producer;
        }

        protected override IGrid<Section> CreateSectionsFrom(Bitmap image)
        {
            IGrid<TerrainType> quantizedMap = this.quantizer.QuantizeImage(image);
            return this.producer.DecideSections(quantizedMap);
        }
    }
}
