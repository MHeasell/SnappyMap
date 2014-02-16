namespace SnappyMap
{
    using System.Collections.Generic;
    using System.Drawing;

    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Database;

    public class IndexedSectionSelector : IIndexedSectionSelector, ISectionDatabase
    {
        private readonly Dictionary<Section, Vector4D> sectionInfo = new Dictionary<Section, Vector4D>();

        private readonly Dictionary<SectionType, List<Section>> store = new Dictionary<SectionType, List<Section>>();

        public void RegisterSection(Section section, SectionType type)
        {
            if (!this.store.ContainsKey(type))
            {
                this.store[type] = new List<Section>();
            }

            this.store[type].Add(section);

            this.sectionInfo[section] = this.ComputeSectionVector(section);
        }

        public Section ChooseSection(SectionType type, Bitmap image, Rectangle imagePortion)
        {
            var featureData = this.ComputeVector(image, imagePortion);

            return this.FindClosest(type, featureData);
        }

        private static Rectangle[] SplitRect(Rectangle area)
        {
            int halfWidth = area.Width / 2;
            int widthRem = area.Width % 2;
            int halfHeight = area.Height / 2;
            int heightRem = area.Height % 2;

            var topLeft = new Rectangle(
                area.X,
                area.Y,
                halfWidth,
                halfHeight);

            var topRight = new Rectangle(
                area.X + halfWidth,
                area.Y,
                halfWidth + widthRem,
                halfHeight);

            var bottomLeft = new Rectangle(
                area.X,
                area.Y + halfHeight,
                halfWidth,
                halfHeight + heightRem);

            var bottomRight = new Rectangle(
                area.X + halfWidth,
                area.Y + halfHeight,
                halfWidth + widthRem,
                halfHeight + heightRem);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private Section FindClosest(SectionType type, Vector4D v)
        {
            Section winner = null;
            double winDistance = double.PositiveInfinity;
            foreach (var section in this.store[type])
            {
                double dist = Vector4D.DistanceSquared(v, this.sectionInfo[section]);
                if (dist < winDistance)
                {
                    winner = section;
                    winDistance = dist;
                }
            }

            return winner;
        }

        private Vector4D ComputeVector(Bitmap image, Rectangle area)
        {
            var rects = SplitRect(area);

            return new Vector4D
                {
                    X = this.ComputeRatio(image, rects[0]),
                    Y = this.ComputeRatio(image, rects[1]),
                    Z = this.ComputeRatio(image, rects[2]),
                    W = this.ComputeRatio(image, rects[3]),
                };
        }

        private Vector4D ComputeSectionVector(Section sect)
        {
            return this.ComputeSectionVector(sect.HeightData);
        }

        private Vector4D ComputeSectionVector(IGrid<int> sect)
        {
            var rect = new Rectangle(0, 0, sect.Width, sect.Height);
            var rects = SplitRect(rect);

            return new Vector4D
                {
                    X = this.ComputeSectionRatio(sect, rects[0]),
                    Y = this.ComputeSectionRatio(sect, rects[1]),
                    Z = this.ComputeSectionRatio(sect, rects[2]),
                    W = this.ComputeSectionRatio(sect, rects[3]),
                };
        }

        private double ComputeSectionRatio(IGrid<int> grid, Rectangle area)
        {
            double mean = 0;
            int count = 0;
            for (int y = 0; y < area.Height; y++)
            {
                for (int x = 0; x < area.Width; x++)
                {
                    double val = grid[x, y] / 255.0;
                    mean = mean + ((1.0 / (count + 1)) * (val - mean));
                    count++;
                }
            }

            return mean;
        }

        private double ComputeRatio(Bitmap image, Rectangle area)
        {
            double mean = 0;
            int count = 0;
            for (int y = 0; y < area.Height; y++)
            {
                for (int x = 0; x < area.Width; x++)
                {
                    var pixel = image.GetPixel(area.X + x, area.Y + y);
                    double val = pixel.R / 255.0;
                    mean = mean + ((1.0 / (count + 1)) * (val - mean));
                    count++;
                }
            }

            return mean;
        }
    }
}