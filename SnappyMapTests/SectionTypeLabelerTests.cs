namespace SnappyMapTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SnappyMap;
    using SnappyMap.Collections;
    using SnappyMap.Data;
    using SnappyMap.Generation;

    [TestClass]
    public class SectionTypeLabelerTests
    {
        [TestMethod]
        public void Label()
        {
            var input = FromString(
                "SSS",
                "SLS",
                "SSS");

            var expected = SectFromString(
                "TL TR",
                "BL BR");

            var labeler = new SectionTypeLabeler();

            var output = labeler.LabelIntersections(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        [TestMethod]
        public void LabelLand()
        {
            var input = FromString(
                "SSSS",
                "SLLS",
                "SLLS",
                "SLLS",
                "SSSS");

            var expected = SectFromString(
                "TL TE TR",
                "LE LL RE",
                "LE LL RE",
                "BL BE BR");

            var labeler = new SectionTypeLabeler();

            var output = labeler.LabelIntersections(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        [TestMethod]
        public void LabelReflexiveCorner()
        {
            var input = FromString(
                "LS",
                "LL");

            var expected = SectFromString(
                "XBL");

            var labeler = new SectionTypeLabeler();

            var output = labeler.LabelIntersections(input);

            Assert.IsTrue(GridMethods.GridEquals(expected, output));
        }

        private static IGrid<TerrainType> FromString(params string[] lines)
        {
            return GridMethods.FromString(Util.CharToType, lines);
        }

        private static IGrid<SectionType> SectFromString(params string[] lines)
        {
            return GridMethods.FromSpacedString(TokenToSectionType, lines);
        }

        private static SectionType TokenToSectionType(string token)
        {
            switch (token)
            {
                case "TR":
                    return SectionType.NorthEastCoast;
                case "TL":
                    return SectionType.NorthWestCoast;
                case "BL":
                    return SectionType.SouthWestCoast;
                case "BR":
                    return SectionType.SouthEastCoast;
                case "LE":
                    return SectionType.WestCoast;
                case "RE":
                    return SectionType.EastCoast;
                case "TE":
                    return SectionType.NorthCoast;
                case "BE":
                    return SectionType.SouthCoast;
                case "SS":
                    return SectionType.Sea;
                case "LL":
                    return SectionType.Land;
                case "XTR":
                    return SectionType.SouthWestReflexiveCoast;
                case "XTL":
                    return SectionType.SouthEastReflexiveCoast;
                case "XBL":
                    return SectionType.NorthEastReflexiveCoast;
                case "XBR":
                    return SectionType.NorthWestReflexiveCoast;
                default:
                    throw new ArgumentException("Unexpected token: " + token);
            }
        }
    }
}
