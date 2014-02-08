namespace SnappyMap.IO
{
    using System.Linq;

    using TAUtil.Sct;

    public class SectionLoader
    {
        private readonly ITileDatabase tileDatabase;

        public SectionLoader(ITileDatabase tileDatabase)
        {
            this.tileDatabase = tileDatabase;
        }

        public Section ReadSection(string filename)
        {
            using (SctReader reader = new SctReader(filename))
            {
                return this.ReadSection(reader);
            }
        }

        public Section ReadSection(ISctSource source)
        {
            int[] tileIds = source.EnumerateTiles().Select(x => this.tileDatabase.AddTile(x)).ToArray();

            Section sct = new Section(source.DataWidth, source.DataHeight);

            var dataEnum = source.EnumerateData().GetEnumerator();

            for (int i = 0; i < source.DataWidth * source.DataHeight; i++)
            {
                dataEnum.MoveNext();
                sct.TileData[i] = this.tileDatabase.GetTileById(tileIds[dataEnum.Current]);
            }

            var attrEnum = source.EnumerateAttrs().GetEnumerator();

            for (int i = 0; i < source.DataWidth * source.DataHeight * 4; i++)
            {
                attrEnum.MoveNext();
                sct.HeightData[i] = attrEnum.Current.Height;
            }

            return sct;
        }
    }
}
