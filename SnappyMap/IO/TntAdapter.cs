namespace SnappyMap.IO
{
    using System.Collections.Generic;
    using System.Linq;

    using TAUtil.Tnt;

    public class TntAdapter : ITntSource
    {
        private readonly Section section;

        private readonly Tile[] tileArray;
        private readonly Dictionary<Tile, int> tileMapping = new Dictionary<Tile, int>();

        public TntAdapter(Section section)
        {
            this.section = section;
            this.tileArray = this.section.TileData.Distinct().ToArray();

            for (int i = 0; i < this.tileArray.Length; i++)
            {
                this.tileMapping[this.tileArray[i]] = i;
            }
        }

        public int DataWidth
        {
            get
            {
                return this.section.TileData.Width;
            }
        }

        public int DataHeight
        {
            get
            {
                return this.section.TileData.Width;
            }
        }

        public int SeaLevel
        {
            get
            {
                return 0;
            }
        }

        public int TileCount
        {
            get
            {
                return this.tileArray.Length;
            }
        }

        public int AnimCount
        {
            get
            {
                return 0;
            }
        }

        public IEnumerable<byte[]> EnumerateTiles()
        {
            return this.tileArray.Select(x => x.Data);
        }

        public IEnumerable<string> EnumerateAnims()
        {
            yield break;
        }

        public IEnumerable<int> EnumerateData()
        {
            return this.section.TileData.Select(x => this.tileMapping[x]);
        }

        public IEnumerable<TileAttr> EnumerateAttrs()
        {
            return this.section.HeightData.Select(
                x => new TileAttr
                {
                    Feature = TileAttr.FeatureNone,
                    Height = (byte)x,
                });
        }

        public MinimapInfo GetMinimap()
        {
            return new MinimapInfo(252, 252, new byte[252 * 252]);
        }
    }
}
