using System.Reflection;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Parse;

public static class ParseHelper
{
    public static T[] Parse<T>(string[] separators, IParser<T> parser, string? input, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<T>();

        return input.Split(separators, stringSplitOptions).Select(number => parser.Parse(number)).ToArray();
    }

    public static T Parse<T>(IParser<T> parser, string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new NullReferenceException();

        return parser.Parse(input);
    }

    public static IEnumerable<T> GetInput<T>(IEnumerable<string> values, IParser2D<T> parser)
    {
        values.Reverse();
        return values.Select((input, y) => parser.Parse(y, input)).SelectMany(item => item);
    }
}