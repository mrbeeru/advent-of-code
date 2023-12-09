namespace AdventOfCode.Helpers
{
    public record class Coords2D(int X, int Y)
    {
        public static readonly Coords2D Up = new(-1, 0);
        public static readonly Coords2D Down = new(1, 0);
        public static readonly Coords2D Left = new(0, -1);
        public static readonly Coords2D Right = new(0, 1);
        public static readonly Coords2D[] Directions = new[] { Up, Down, Left, Right };

        public static Coords2D operator +(Coords2D a, Coords2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Coords2D operator -(Coords2D a, Coords2D b) => new(a.X - b.X, a.Y - b.Y);
    }
}
