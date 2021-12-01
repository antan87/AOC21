using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Parse;

public class StringParser : IParser<string>
{
    public string Parse(string value) => value;
}