namespace SnappyMap
{
    public class Tile
    {
        public Tile(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data { get; private set; }
    }
}