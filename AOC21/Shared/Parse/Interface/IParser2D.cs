namespace AOC21.Shared.Parse.Interface;

public interface IParser2D<T>
{
    T[] Parse(int y, string value);
}