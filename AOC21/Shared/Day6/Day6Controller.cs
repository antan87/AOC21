using AOC21.Shared.Parse;

namespace AOC21.Shared.Day6
{
    public class Day6Controller
    {
        public long Run(int days, string input)
        {
            var fishes = ParseHelper.Parse(new string[] { "," }, ParserCreator.Int32Parser, input, StringSplitOptions.TrimEntries).Select(number => new LanternFish(number, 1));
            var groupedFishes = fishes.GroupBy(item => item.DaysLeft).Select(group => new LanternFish(group.First().DaysLeft, group.Count()));

            var result = this.Calculator(days, groupedFishes.ToList());

            return result;
        }

        private long Calculator(int days, List<LanternFish> fishes)
        {
            while (days != 0)
            {
                var newFishes = fishes.Where(fish => fish.DaysLeft == 0).Sum(item => item.CountOfFishes);
                fishes = fishes.Select(fish => fish.Iterator())
                    .GroupBy(item => item.DaysLeft)
                    .Select(group => new LanternFish(group.First().DaysLeft, group.Sum(item => item.CountOfFishes)))
                    .ToList();
                fishes.Add(new LanternFish(8, newFishes));

                days--;
            }

            return fishes.Sum(fish => fish.CountOfFishes);
        }
    }
}
