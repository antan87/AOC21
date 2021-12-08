using AOC21.Shared.Algebra.Models;
using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day5
{
    public class DirectionParser : IParser<IEnumerable<Direction>>
    {
        private readonly bool useDiagonal;

        public DirectionParser(bool useDiagonal)
        {
            this.useDiagonal = useDiagonal;
        }

        IEnumerable<Direction> IParser<IEnumerable<Direction>>.Parse(string input)
        {
            var splits = ParseHelper.Parse<string>(new string[] { "->", Environment.NewLine }, ParserCreator.StringParser, input, StringSplitOptions.TrimEntries);
            var points = splits.Select(item => ParseHelper.Parse<int>(new string[] { "," }, ParserCreator.Int32Parser, item)).Select(item => new Point2D(item[0], item[1])).ToList();

            for (int index = 0; index < points.Count(); index += 2)
            {
                var selectedPoints = points.Skip(index).Take(2).ToList();

                yield return new Direction(selectedPoints.First(), selectedPoints.Last(),this.useDiagonal);
            }
        }
    }
}
