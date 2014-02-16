using System;

using SnappyMap;
using SnappyMap.Data;

static internal class Util
{
    public static TerrainType CharToType(char c)
    {
        switch (c)
        {
            case 'S':
                return TerrainType.Sea;
            case 'L':
                return TerrainType.Land;
            default:
                throw new ArgumentException("Unrecognised character: " + c);
        }
    }
}