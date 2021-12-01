using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Parse;

public class Int32Parser : IParser<int>
{
    public int Parse(string value) => int.Parse(value);
}