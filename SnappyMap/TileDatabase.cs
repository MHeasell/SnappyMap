namespace SnappyMap
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class TileDatabase : ITileDatabase
    {
        private readonly HashAlgorithm hashAlgorithm;

        private readonly List<Tile> tiles = new List<Tile>(); 

        private readonly Dictionary<string, int> hashIndex = new Dictionary<string, int>();

        public TileDatabase(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }

        public int AddTile(byte[] tileData)
        {
            return this.AddTile(new Tile(tileData));
        }

        public int AddTile(Tile tile)
        {
            var hashData = this.hashAlgorithm.ComputeHash(tile.Data);
            var hashString = Convert.ToBase64String(hashData);

            if (!this.hashIndex.ContainsKey(hashString))
            {
                this.tiles.Add(tile);
                this.hashIndex[hashString] = this.tiles.Count - 1;
            }

            return this.hashIndex[hashString];
        }

        public Tile GetTileById(int id)
        {
            return this.tiles[id];
        }
    }
}