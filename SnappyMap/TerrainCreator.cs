namespace SnappyMap
{
    using System.Drawing;

    using SnappyMap.Collections;

    public class TerrainCreator : ITerrainCreator
    {
        private readonly IMapQuantizer quantizer;

        private readonly ISectionDecider producer;

        private readonly ISectionGridRenderer renderer;

        public TerrainCreator(
            IMapQuantizer quantizer,
            ISectionDecider producer,
            ISectionGridRenderer renderer)
        {
            this.quantizer = quantizer;
            this.producer = producer;
            this.renderer = renderer;
        }

        public Section CreateTerrainFrom(Bitmap image)
        {
            IGrid<TerrainType> quantizedMap = this.quantizer.QuantizeImage(image);

            IGrid<Section> sectionGrid = this.producer.DecideSections(quantizedMap);

            Section map = this.renderer.Render(sectionGrid);

            return map;
        }
    }
}
