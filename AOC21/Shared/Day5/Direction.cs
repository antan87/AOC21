using AOC21.Shared.Algebra.Models;

namespace AOC21.Shared.Day5
{
    public record Direction(Point2D Point, Point2D PointTwo, bool UseDiagonal)
    {
        public List<Point2D> GetSteps()
        {
            if (!UseDiagonal && Point.X != PointTwo.X && Point.Y != PointTwo.Y)
                return new List<Point2D>();

            List<Point2D> points = new List<Point2D> { Point };

            Point2D cursorPoint = new Point2D(Point.X, Point.Y);

            while (!cursorPoint.Equals(PointTwo))
            {
                cursorPoint = Recursive(cursorPoint, PointTwo);
                points.Add(cursorPoint);
            }

            return points;


            Point2D Recursive(Point2D point, Point2D finalPoint)
            {
                if (point.X < finalPoint.X)
                    return new Point2D(point.X + 1, point.Y);

                else if (point.X > finalPoint.X)
                    return new Point2D(point.X - 1, point.Y);

                else if (point.Y < finalPoint.Y)
                    return new Point2D(point.X, point.Y + 1);

                else if (point.Y > finalPoint.Y)
                    return new Point2D(point.X, point.Y - 1);

                return finalPoint;
            }
        }
    }
}
