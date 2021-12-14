using AOC21.Shared.Algebra.Models;
using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day9
{
    public class Day9Controller
    {
        public int Run(string input)
        {
            var locations = input.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser)
                                 .GetInput(new LocatonParser())
                                 .ToList();

            var resultLocations = locations.Where(location => location.GetNeighbors(locations).All(neigbor => neigbor.Number > location.Number));

            return resultLocations.Sum(item => item.Number + +1);
        }

        public int Run2(string input)
        {
            var locations = input.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser)
                                 .GetInput(new LocatonParser())
                                 .ToList();



            return locations.Select(location => location.GetBasins(locations).Count())
                .Where(count => count > 0)
                .OrderByDescending(count => count)
                .Take(3)
                .Aggregate((a, b) => a * b);
        }

        private class LocatonParser : IParser2D<Location>
        {
            public Location[] Parse(int y, string value) =>
            value.Select((charachter, x) => GetVector(charachter, new Point2D(x, y))).ToArray();

            private Location GetVector(char charachter, Point2D point)
            {
                return new(point, int.Parse(charachter.ToString()));
            }
        }

        public record Location(Point2D Point, int Number)
        {

            public IEnumerable<Location> GetBasins(IEnumerable<Location> locations)
            {
                var isSmaller = IsSmallerThanNeighbors(locations);
                if (!isSmaller)
                    return Enumerable.Empty<Location>();

                List<Location> result = new List<Location>() { this };

                var newLocations = locations.Where(item => !item.Equals(this));

                Recursive(GetNeighbors(newLocations), newLocations, result);

                return result;

                static void Recursive(IEnumerable<Location> subjects, IEnumerable<Location> locations, List<Location> result)
                {
                    var list = new List<Location>();
                    foreach (var location in subjects)
                    {
                        if (result.Any(item => item.Equals(location)))
                            continue;

                        var isSmaller = location.IsSmallerThanNeighbors(locations);
                        if (isSmaller)
                        {
                            result.Add(location);
                            locations = locations.Where(item => !item.Equals(location));
                            list.AddRange(location.GetNeighbors(locations));
                        }
                    }

                    if (list.Any())
                        Recursive(list.Distinct(), locations, result);
                }

            }

            public bool IsSmallerThanNeighbors(IEnumerable<Location> locations)
            {
                if (this.Number == 9)
                    return false;

                return GetNeighbors(locations).All(neigbor => neigbor.Number >= this.Number);
            }

            public IEnumerable<Location> GetNeighbors(IEnumerable<Location> neighbors) => neighbors.Where(location =>
            this.Point.X + 1 == location.Point.X && location.Point.Y == this.Point.Y ||
            this.Point.X - 1 == location.Point.X && location.Point.Y == this.Point.Y ||
            this.Point.X == location.Point.X && location.Point.Y == this.Point.Y + 1 ||
            this.Point.X == location.Point.X && location.Point.Y == this.Point.Y - 1);
        }
    }


}
