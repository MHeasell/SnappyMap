﻿namespace SnappyMap.IO
{
    public interface ITileDatabase
    {
        int AddTile(byte[] tileData);

        int AddTile(Tile tile);

        Tile GetTileById(int id);
    }
}