namespace SnappyMap
{
    using System;
    using System.Drawing;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            Bitmap image = new Bitmap(args[0]);

            ITerrainCreator creator = CreateTerrainCreator();

            Section terrain = creator.CreateTerrainFrom(image);

            ISectionSerializer serializer = CreateSectionSerializer();

            using (Stream output = File.Create(args[1]))
            {
                serializer.WriteSection(output, terrain);
            }
        }

        private static ITerrainCreator CreateTerrainCreator()
        {
            throw new NotImplementedException();
        }

        private static ISectionSerializer CreateSectionSerializer()
        {
            throw new NotImplementedException();
        }
    }
}
