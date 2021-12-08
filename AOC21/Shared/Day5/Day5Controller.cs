using AOC21.Shared.Parse;

namespace AOC21.Shared.Day5
{
    public class Day5Controller
    {
        public (int part1, int part2) Run(string input)
        {
            var directions = ParseHelper.Parse<IEnumerable<Direction>>(new DirectionParser(false), input);

            var part1 =  this.Calculator(directions.ToList());
            var directions2 = ParseHelper.Parse<IEnumerable<Direction>>(new DirectionParser(true), input);
            var part2 = this.Calculator(directions2.ToList());

            return  (part1, part2);
        }

        private int Calculator(List<Direction> directions)
        {
            var items = directions.SelectMany(item => item.GetSteps()).GroupBy(item => item).Where(item => item.Count() > 1);
            return items.Count();
        }
    }
}
