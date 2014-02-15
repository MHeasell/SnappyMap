namespace SnappyMap
{
    internal struct Vector4D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double W { get; set; }

        public static Vector4D operator -(Vector4D a, Vector4D b)
        {
            return new Vector4D
                {
                    X = a.X - b.X,
                    Y = a.Y - b.Y,
                    Z = a.Z - b.Z,
                    W = a.W - b.W,
                };
        }

        public static double DistanceSquared(Vector4D a, Vector4D b)
        {
            return (b - a).LengthSquared();
        }

        public double LengthSquared()
        {
            return (this.X * this.X)
                + (this.Y * this.Y)
                + (this.Z * this.Z)
                + (this.W * this.W);
        }
    }
}