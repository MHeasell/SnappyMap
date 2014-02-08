namespace SnappyMap
{
    using System;
    using System.Collections.Generic;

    using SnappyMap.Collections;

    public class SectionTypeLabeler : ISectionTypeLabeler
    {
        private static Dictionary<Quadrant, SectionType> TypeLookup = new Dictionary<Quadrant, SectionType>
            {
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Sea, TerrainType.Sea), SectionType.Sea },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Sea, TerrainType.Sea), SectionType.BottomRightCorner },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Sea, TerrainType.Sea), SectionType.BottomLeftCorner },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Land, TerrainType.Sea), SectionType.TopRightCorner },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Sea, TerrainType.Land), SectionType.TopLeftCorner },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Sea, TerrainType.Sea), SectionType.BottomEdge },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Land, TerrainType.Land), SectionType.TopEdge },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Land, TerrainType.Sea), SectionType.RightEdge },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Sea, TerrainType.Land), SectionType.LeftEdge },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Land, TerrainType.Sea), SectionType.ReflexiveTopLeftCorner },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Sea, TerrainType.Land), SectionType.ReflexiveTopRightCorner },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Land, TerrainType.Land), SectionType.ReflexiveBottomLeftCorner },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Land, TerrainType.Land), SectionType.ReflexiveBottomRightCorner },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Land, TerrainType.Land), SectionType.Land },
            };

        public IGrid<SectionType> LabelIntersections(IGrid<TerrainType> layout)
        {
            var grid = new Grid<SectionType>(layout.Width - 1, layout.Height - 1);

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    var quad = new Quadrant(
                        layout[x, y],
                        layout[x + 1, y],
                        layout[x, y + 1],
                        layout[x + 1, y + 1]);

                    grid[x, y] = this.LabelIntersection(quad);
                }
            }

            return grid;
        }

        private SectionType LabelIntersection(Quadrant intersection)
        {
            return TypeLookup[intersection];
        }

        private struct Quadrant
        {
            public Quadrant(TerrainType topLeft, TerrainType topRight, TerrainType bottomLeft, TerrainType bottomRight)
                : this()
            {
                this.TopLeft = topLeft;
                this.TopRight = topRight;
                this.BottomLeft = bottomLeft;
                this.BottomRight = bottomRight;
            }

            public TerrainType TopLeft { get; set; }

            public TerrainType TopRight { get; set; }

            public TerrainType BottomLeft { get; set; }

            public TerrainType BottomRight { get; set; }
        }
    }
}