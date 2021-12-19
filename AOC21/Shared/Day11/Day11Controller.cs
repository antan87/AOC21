using AOC21.Shared.Algebra.Models;
using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day11
{
    public class Day11Controller
    {

        public int Run(string input)
        {
            var lines = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, input, StringSplitOptions.TrimEntries);
            var octopuses = ParseHelper.GetInput(lines, new DumboOctopusParser());
            return new DumboOctopusEventHandler(octopuses.ToList()).Run(100);
        }

        public int Run2(string input)
        {
            var lines = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, input, StringSplitOptions.TrimEntries);
            var octopuses = ParseHelper.GetInput(lines, new DumboOctopusParser());
            return new DumboOctopusEventHandler(octopuses.ToList()).Run2();
        }

        private class DumboOctopusParser : IParser2D<DumboOctopus>
        {
            public DumboOctopus[] Parse(int y, string value) =>
            value.Select((charachter, x) => GetVector(charachter, new Point2D(x, y))).ToArray();

            private DumboOctopus GetVector(char charachter, Point2D point)
            {
                return new(point, int.Parse(charachter.ToString()));
            }
        }

        private class DumboOctopusEventHandler
        {
            private event FlashEventHandler? FlashEvent;
            private int flashCounter;
            public DumboOctopusEventHandler(List<DumboOctopus> octopus)
            {
                Octopus = octopus;
                Octopus.ForEach(octopus =>
                {
                    octopus.stepEvent += this.OctopusStepEvent;
                    FlashEvent += octopus.FlashEvent;
                });
            }

            public int Run(int steps)
            {
                Enumerable.Range(0, steps).ToList().ForEach(index =>
                {
                    Octopus.ForEach(octupus =>
                    {
                        octupus.Step();
                    });

                    Flash();

                    Octopus.ForEach(octupus =>
                    {
                        octupus.Reset();
                    });
                });

                return this.flashCounter;
            }

            public int Run2()
            {
                int index = 0;
                while (true)
                {
                    index += 1;
                    Octopus.ForEach(octupus =>
                    {
                        octupus.Step();
                    });

                    Flash();

                    Octopus.ForEach(octupus =>
                    {
                        octupus.Reset();
                    });

                    if (this.Octopus.All(item => item.Number == 0))
                    {
                        return index;
                    }
                }
            }

            public List<DumboOctopus> Octopus { get; }

            private List<DumboOctopus> flashes = new List<DumboOctopus>();

            public void Flash()
            {
                while (flashes.Any())
                {
                    DumboOctopus octopus = flashes.First();
                    flashes.Remove(octopus);
                    octopus.MarkAsFlashed();
                    octopus.Number = 0;
                    FlashEvent?.Invoke(this, octopus);
                    flashCounter++;
                }
            }
            public void OctopusStepEvent(object _, DumboOctopus octopus)
            {
                if (octopus.Number > 9 && !octopus.Flashed && !flashes.Any(item => item.Point.Equals(octopus.Point)))
                    flashes.Add(octopus);
            }
        }

        private delegate void FlashEventHandler(object sender, DumboOctopus e);
        private delegate void StepEventHandler(object sender, DumboOctopus e);
        private class DumboOctopus
        {
            public event StepEventHandler? stepEvent;
            private List<(IntPosition xP, IntPosition yp)> diagonalList = new List<(IntPosition xP, IntPosition yp)>();

            public DumboOctopus(Point2D point, int number)
            {
                Point = point;
                Number = number;
            }

            public bool Flashed { get; set; }
            public Point2D Point { get; }
            public int Number { get; set; }

            public void MarkAsFlashed()
            {
                Flashed = true;
            }

            public void Reset()
            {
                Flashed = false;
                diagonalList = new List<(IntPosition xP, IntPosition yp)>();
            }

            public void Step()
            {
                if (!this.Flashed)
                {
                    Number += 1;
                    stepEvent?.Invoke(this, this);
                }
            }

            public void FlashEvent(object _, DumboOctopus octopus)
            {
                if (octopus.Point.Equals(Point) || this.Flashed)
                    return;

                if (Point.X + 1 == octopus.Point.X && Point.Y == octopus.Point.Y)
                {
                    Step();
                    return;
                }

                else if (Point.X - 1 == octopus.Point.X && Point.Y == octopus.Point.Y)
                {
                    Step();
                    return;
                }

                else if (Point.Y + 1 == octopus.Point.Y && Point.X == octopus.Point.X)
                {
                    Step();
                    return;
                }

                else if (Point.Y - 1 == octopus.Point.Y && Point.X == octopus.Point.X)
                {
                    Step();
                    return;
                }

                (int dx, int dy) = GetDiagonalXy(this, octopus);
                if (IsDiagonal(dx, dy))
                {
                    if (!diagonalList.Any(item => item.xP == dx.GetIntPosition() && item.yp == dy.GetIntPosition()))
                    {
                        diagonalList.Add((dx.GetIntPosition(), dy.GetIntPosition()));
                        Step();
                    }
                }
            }

            private static bool IsDiagonal(int dx, int dy)
            {
                return (Math.Abs(dx) == Math.Abs(dy)) && (Math.Abs(dx) + Math.Abs(dy)) < 3;
            }

            private (int dx, int dy) GetDiagonalXy(DumboOctopus octopus, DumboOctopus octopus2)
            {
                var dy = octopus2.Point.Y - octopus.Point.Y;
                var dx = octopus2.Point.X - octopus.Point.X;

                return (dx, dy);
            }
        }
    }

    public static class IntegerExtensions
    {
        public static IntPosition GetIntPosition(this int integer)
        {
            if (integer == 0)
                return IntPosition.Zero;

            if (integer > 0)
                return IntPosition.Positive;

            return IntPosition.Negative;
        }
    }

    public enum IntPosition
    {
        Zero,
        Negative,
        Positive
    }
}
