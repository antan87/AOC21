using AOC21.Shared.Algebra.Interface;

namespace AOC21.Shared.Algebra.Models;

public readonly struct Point2D : IPoint
{
    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
}