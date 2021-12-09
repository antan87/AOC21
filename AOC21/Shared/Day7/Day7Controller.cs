using AOC21.Shared.Parse;

namespace AOC21.Shared.Day7
{
    public class Day7Controller
    {
        public int Run(string input)
        {
            var crabs = ParseHelper.Parse(new string[] { "," }, ParserCreator.Int32Parser, input, StringSplitOptions.TrimEntries).Select(number => new Crab(number));
            return Calculator(crabs);
        }

        public int Run2(string input)
        {
            var crabs = ParseHelper.Parse(new string[] { "," }, ParserCreator.Int32Parser, input, StringSplitOptions.TrimEntries).Select(number => new Crab(number, true));
            return Calculator(crabs);
        }

        private int Calculator(IEnumerable<Crab> crabs)
        {
            int position = GetMedian(crabs.Select(item => item.Position));
            var sum = crabs.Sum(item => item.Distance(position));
            int lowPosition = position;
            int highPosition = position;
            Recursive(crabs, () => --lowPosition, ref sum);
            Recursive(crabs, () => ++highPosition, ref sum);

            return sum;

            static void Recursive(IEnumerable<Crab> crabs, Func<int> positionFunc, ref int sum)
            {
                int position = positionFunc();
                var newSum = crabs.Sum(item => item.Distance(position));
                if (newSum < sum)
                {
                    sum = newSum;
                    Recursive(crabs, positionFunc, ref sum);
                }
            }
        }

        public static int GetMedian(IEnumerable<int> list)
        {
            int[] tempArray = list.ToArray();
            int count = tempArray.Length;
            Array.Sort(tempArray);
            int medianValue;
            if (count % 2 == 0)
            {
                int middleElement1 = tempArray[(count / 2) - 1];
                int middleElement2 = tempArray[(count / 2)];
                medianValue = (middleElement1 + middleElement2) / 2;
            }
            else
                medianValue = tempArray[(count / 2)];

            return medianValue;
        }
    }
}
