namespace AOC21.Shared.Parse.Interface;

public interface IParser<T>
{
    T Parse(string value);
}