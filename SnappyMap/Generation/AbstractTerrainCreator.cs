namespace SnappyMap.Generation
{
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Rendering;

    public abstract class AbstractTerrainCreator : ITerrainCreator
    {
        private readonly ISectionGridRenderer renderer = new SectionGridRenderer();

        public Section CreateTerrainFrom(Bitmap image)
        {
            return this.renderer.Render(this.CreateSectionsFrom(image));
        }

        protected abstract IGrid<Section> CreateSectionsFrom(Bitmap image);
    }
}
