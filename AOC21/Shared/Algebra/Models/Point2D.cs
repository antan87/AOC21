using AOC21.Shared.Algebra.Interface;

namespace AOC21.Shared.Algebra.Models;

public readonly struct Point2D : IPoint,IEquatable<Point2D>
{
    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public bool Equals(Point2D point, Point2D point2)
    {
        int? ddc = null;
        return point.X == point2.X && point.Y == point2.Y;
    }

    public bool Equals(Point2D other)
    {
        return this.X == other.X && this.Y == other.Y;

    }
}