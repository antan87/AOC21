using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day8
{
    public class Day8Controller
    {
        public static Dictionary<int, List<SevenDigitNumberSegment>> Numbers = new Dictionary<int, List<SevenDigitNumberSegment>>
        {
            {0, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.TopRight, SevenDigitNumberSegment.BottomLeft, SevenDigitNumberSegment.BottomRight,  SevenDigitNumberSegment.Bottom } },
            {1, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.TopRight, SevenDigitNumberSegment.BottomRight} },
            {2, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopRight , SevenDigitNumberSegment.Middel, SevenDigitNumberSegment.BottomLeft, SevenDigitNumberSegment.Bottom } },
            {3, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopRight , SevenDigitNumberSegment.Middel, SevenDigitNumberSegment.BottomRight, SevenDigitNumberSegment.Bottom } },
            {4, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.TopRight , SevenDigitNumberSegment.Middel, SevenDigitNumberSegment.BottomRight } },
            {5, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.Middel, SevenDigitNumberSegment.BottomRight ,SevenDigitNumberSegment.Bottom } },
            {6, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.Middel, SevenDigitNumberSegment.BottomLeft, SevenDigitNumberSegment.BottomRight ,SevenDigitNumberSegment.Bottom } },
            {7, new List<SevenDigitNumberSegment>  { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopRight, SevenDigitNumberSegment.BottomRight } },
            {8, new List<SevenDigitNumberSegment> { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.TopRight,SevenDigitNumberSegment.Middel , SevenDigitNumberSegment.BottomLeft, SevenDigitNumberSegment.BottomRight,   SevenDigitNumberSegment.Bottom } },
            {9, new List<SevenDigitNumberSegment> { SevenDigitNumberSegment.Top, SevenDigitNumberSegment.TopLeft, SevenDigitNumberSegment.TopRight,  SevenDigitNumberSegment.Middel,  SevenDigitNumberSegment.BottomRight, SevenDigitNumberSegment.Bottom } },
        };

        public int Run(string input)
        {
            var array = ParseHelper.Parse(new SevenDigitsParser(), input);


            return array.Sum(stringItem => stringItem.Output.Count(stringItem =>
                                             stringItem.Count() == Numbers[1].Count ||
                                             stringItem.Count() == Numbers[4].Count ||
                                             stringItem.Count() == Numbers[7].Count ||
                                             stringItem.Count() == Numbers[8].Count));

        }


        public int Run2(string input)
        {
            var rows = ParseHelper.Parse(new SevenDigitsParser(), input);

            var numbers = new List<int>();
            foreach (InputOutput row in rows)
            {
                var number = int.Parse(string.Join(null, GetNumbers(row).Select(number => number.ToString())));
                numbers.Add(number);
            }

            return numbers.Sum();
        }

        private static IEnumerable<int> GetNumbers(InputOutput row)
        {
            var letters = new List<SegmentItem>();

            var currentNumbers = new List<NumberItem>();
            var numberOneInput = row.Input.First(item => item.Length == Numbers[1].Count()).Select(item => item.ToString()).Where(item => !letters.Any(letter => letter.Letter == item));
            var topRightLetter = numberOneInput.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 8);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.TopRight, topRightLetter));
            var bottomRightLetter = numberOneInput.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 9);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.BottomRight, bottomRightLetter));
            var numberSevenInput = row.Input.First(item => item.Length == Numbers[7].Count()).Select(item => item.ToString()).Where(item => !letters.Any(letter => letter.Letter == item));
            letters.Add(new SegmentItem(SevenDigitNumberSegment.Top, numberSevenInput.First().ToString()));

            var numberFourInputs = row.Input.First(item => item.Length == Numbers[4].Count()).Select(item => item.ToString()).Where(item => !letters.Any(letter => letter.Letter == item));
            var topLeftLetter = numberFourInputs.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 6);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.TopLeft, topLeftLetter));

            var middeltLetter = numberFourInputs.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 7);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.Middel, middeltLetter));

            var numberEightInputs = row.Input.First(item => item.Length == Numbers[8].Count()).Select(item => item.ToString()).Where(item => !letters.Any(letter => letter.Letter == item));
            var bottomLeftLetter = numberEightInputs.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 4);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.BottomLeft, bottomLeftLetter));

            var bottomLetter = numberEightInputs.First(item => row.Input.SelectMany(letterString => letterString.ToString()).Where(letterString => letterString.ToString() == item).Count() == 7);
            letters.Add(new SegmentItem(SevenDigitNumberSegment.Bottom, bottomLetter));

            foreach (var output in row.Output)
            {
                var segments = output.Select(letter => letter.ToString()).Select(item => letters.First(segment => segment.Letter == item));
                yield return Numbers.First(number => number.Value.Count() == segments.Count() && segments.All(item => number.Value.Any(inner => inner == item.Segment))).Key;
            }
        }

        public class SevenDigitsParser : IParser<InputOutput[]>
        {
            public InputOutput[] Parse(string input)
            {
                var array = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, input);
                return GetInputAndOutput(array).ToArray();

                IEnumerable<InputOutput> GetInputAndOutput(string[] array)
                {
                    foreach (var stringItem in array)
                    {
                        var delimiter = ParseHelper.Parse(new string[] { "|" }, ParserCreator.StringParser, stringItem, StringSplitOptions.TrimEntries);
                        var inputs = ParseHelper.Parse(new string[] { " " }, ParserCreator.StringParser, delimiter[0]);
                        var outputs = ParseHelper.Parse(new string[] { " " }, ParserCreator.StringParser, delimiter[1]);
                        yield return new InputOutput(inputs, outputs);
                    }
                }
            }

        }

        public record SegmentItem(SevenDigitNumberSegment Segment, string Letter);
        public record NumberItem
        {
            public NumberItem(int number, IEnumerable<SevenDigitNumberSegment> segments, string[] letters)
            {
                if (segments.Count() != letters.Count())
                    throw new ArgumentException($"Segments {segments.Count()} do not match letters {letters.Count()}");
                Number = number;
                Segments = segments;
                Letters = segments.Select((item, index) => new SegmentItem(item, letters[index]));
            }
            public IEnumerable<SegmentItem> Letters { get; }
            public int Number { get; }
            public IEnumerable<SevenDigitNumberSegment> Segments { get; } = Enumerable.Empty<SevenDigitNumberSegment>();

            public NumberItem New(string[] letters) => new(this.Number, this.Segments, letters);
        }

        public record SevenDigit
        {
            public SevenDigit(IEnumerable<SevenDigitNumberSegment> segments, string[] letters)
            {
                if (segments.Count() != letters.Count())
                    throw new ArgumentException($"Segments {segments.Count()} do not match letters {letters.Count()}");
                Segments = segments;
                Letters = segments.Select((item, index) => new SegmentItem(item, letters[index]));
            }
            public IEnumerable<SegmentItem> Letters { get; }
            public IEnumerable<SevenDigitNumberSegment> Segments { get; } = Enumerable.Empty<SevenDigitNumberSegment>();

            public SevenDigit New(string[] letters) => new(this.Segments, letters);
        }
        public record InputOutput(string[] Input, string[] Output);


        public enum SevenDigitNumberSegment
        {
            Top,
            TopLeft,
            TopRight,
            Middel,
            BottomLeft,
            BottomRight,
            Bottom
        }
    }
}
