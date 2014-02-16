namespace SnappyMapTests
{
    using System.Security.Cryptography;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SnappyMap;
    using SnappyMap.Data;
    using SnappyMap.Database;

    [TestClass]
    public class TileDatabaseTests
    {
        private TileDatabase db;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new TileDatabase(SHA1.Create());
        }

        [TestMethod]
        public void PutGet()
        {
            var tile = new Tile(new byte[] { 1, 2, 3 });

            int id = this.db.AddTile(tile);

            var result = this.db.GetTileById(id);

            Assert.AreSame(tile, result);
        }

        [TestMethod]
        public void PutGetSame()
        {
            var tile1 = new Tile(new byte[] { 1, 2, 3 });
            var tile2 = new Tile(new byte[] { 1, 2, 3 });

            int id1 = this.db.AddTile(tile1);
            int id2 = this.db.AddTile(tile2);

            Assert.AreSame(tile1, this.db.GetTileById(id1));
            Assert.AreSame(tile1, this.db.GetTileById(id2));
        }


        [TestMethod]
        public void PutGetDifferent()
        {
            var tile1 = new Tile(new byte[] { 1, 2, 3 });
            var tile2 = new Tile(new byte[] { 1, 2, 4 });

            int id1 = this.db.AddTile(tile1);
            int id2 = this.db.AddTile(tile2);

            Assert.AreSame(tile1, this.db.GetTileById(id1));
            Assert.AreSame(tile2, this.db.GetTileById(id2));
        }
    }
}
