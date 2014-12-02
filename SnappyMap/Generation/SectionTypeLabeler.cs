namespace SnappyMap.Generation
{
    using System.Collections.Generic;

    using SnappyMap.Collections;
    using SnappyMap.Data;

    public class SectionTypeLabeler : ISectionTypeLabeler
    {
        private static readonly Dictionary<Quadrant, SectionType> TypeLookup = new Dictionary<Quadrant, SectionType>
            {
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Sea, TerrainType.Sea), SectionType.Sea },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Sea, TerrainType.Sea), SectionType.SouthEastCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Sea, TerrainType.Sea), SectionType.SouthWestCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Land, TerrainType.Sea), SectionType.NorthEastCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Sea, TerrainType.Land), SectionType.NorthWestCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Sea, TerrainType.Sea), SectionType.SouthCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Sea, TerrainType.Land, TerrainType.Land), SectionType.NorthCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Land, TerrainType.Sea), SectionType.EastCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Sea, TerrainType.Land), SectionType.WestCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Land, TerrainType.Sea), SectionType.SouthEastReflexiveCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Sea, TerrainType.Land), SectionType.SouthWestReflexiveCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Sea, TerrainType.Land, TerrainType.Land), SectionType.NorthEastReflexiveCoast },
                { new Quadrant(TerrainType.Sea, TerrainType.Land, TerrainType.Land, TerrainType.Land), SectionType.NorthWestReflexiveCoast },
                { new Quadrant(TerrainType.Land, TerrainType.Land, TerrainType.Land, TerrainType.Land), SectionType.Land },
            };

        public IGrid<SectionType> LabelIntersections(IGrid<TerrainType> layout)
        {
            // The intersection of a quadrant where tiles meet on a corner
            // has no equivalent section type.
            // To avoid this, we change the layout before processing
            // to get rid of diagonal meeting points.
            layout = FilterDiagonals(layout);

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

        private static IGrid<TerrainType> FilterDiagonals(IGrid<TerrainType> input)
        {
            var output = new Grid<TerrainType>(input);

            // Here we go through the output grid in-place
            // to remove any "diagonal" tile intersections,
            // which we don't have sections for.
            // We always modify the bottom right tile in each case,
            // which propagates the error downwards,
            // so any new diagonal intersections created by "fixing" this one
            // will always be in a place we have yet to scan.
            for (int y = 0; y < output.Height - 1; y++)
            {
                for (int x = 0; x < output.Width - 1; x++)
                {
                    if (output[x, y] == TerrainType.Land
                        && output[x + 1, y] == TerrainType.Sea
                        && output[x, y + 1] == TerrainType.Sea
                        && output[x + 1, y + 1] == TerrainType.Land)
                    {
                        output[x + 1, y + 1] = TerrainType.Sea;
                    }
                    else if (output[x, y] == TerrainType.Sea
                        && output[x + 1, y] == TerrainType.Land
                        && output[x, y + 1] == TerrainType.Land
                        && output[x + 1, y + 1] == TerrainType.Sea)
                    {
                        output[x, y] = TerrainType.Land;
                    }
                }
            }

            return output;
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