using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;
using System.Linq;

namespace AOC21.Shared.Day10;

public class Day10Controller
{
    public long Run(string input)
    {
        var list = ParseHelper.Parse(new Parser(), input);

        return list.Select(characters => new Chunk(characters))
            .Select(chunk => chunk.Collect())
            .Where(item => !item.IsValid)
            .GroupBy(item => item.Points)
            .Where(item => item.Key > 0)
            .Sum(group => group.Count() * group.Key);
    }

    public long Run2(string input)
    {
        var list = ParseHelper.Parse(new Parser(), input);

        var array = list.Select(characters => new Chunk(characters))
            .Select(chunk => chunk.Collect())
            .Where(chunk => chunk.IsValid )
            .OrderByDescending(item => item.Points)
            .ToArray();

        return array[array.Length / 2].Points;
            

    }

    private class Parser : IParser<IEnumerable<IEnumerable<ICharacter>>>
    {
        public IEnumerable<IEnumerable<ICharacter>> Parse(string input)
        {
            var array = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, input, StringSplitOptions.TrimEntries);
            return GetCharacters(array).ToArray();

            IEnumerable<IEnumerable<ICharacter>> GetCharacters(string[] array)
            {
              var s =  array.Select(item => item.Select(character => GetCharacterObject(character)));
                 return s;
            }
        }

        private static ICharacter GetCharacterObject(char character)
        {
            switch (character)
            {
                case '[':
                    return new LeftOpener(character, ']');

                case '(':
                    return new LeftOpener(character, ')');

                case '{':
                    return new LeftOpener(character, '}');

                case '<':
                    return new LeftOpener(character, '>');

                case ']':
                    return new RightClose(character);

                case ')':
                    return new RightClose(character);

                case '}':
                    return new RightClose(character);

                case '>':
                    return new RightClose(character);

                default:
                    throw new NotImplementedException($"Missing {character}");
            }
        }


    }
}

public record Chunk(IEnumerable<ICharacter> Characters, bool IsValid = true)
{
    public long Points { get; init; }
    public Chunk Collect()
    {
        List< LeftOpener> openers= new List<LeftOpener>();
        foreach (var character in Characters)
        {
            if(character is RightClose)
            {
                 if(!openers.Any() || !openers.First().ExpectedCloseCharacter.Equals(character.Character))
                    return new Chunk(Characters,false) { Points = GetWrongCloserPoint(character) };

                openers.RemoveAt(0);
            } else 
            {
                openers.Insert(0,(LeftOpener)character);
            }
        }

        //Characters.Concat(openers.Select(item => new RightClose(item.ExpectedCloseCharacter)));
        var result = new Chunk(Characters) { Points = openers.Select(item => 
             GetMissingCloserPoint( item))
            .Aggregate(0, (long a, long b) => {
                return a * 5 + b;
            } )};

        return result;

     static int GetWrongCloserPoint(ICharacter character)
        {
            switch (character.Character)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;

                default:
                    throw new NotImplementedException($"Missing {character}");
            }
        }

        static long GetMissingCloserPoint(LeftOpener character)
        {
            switch (character.ExpectedCloseCharacter)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;

                default:
                    throw new NotImplementedException($"Missing {character}");
            }
        }
    }
}

public record LeftOpener(char Character, char ExpectedCloseCharacter) : ICharacter;
public record RightClose(char Character) : ICharacter;


public interface ICharacter
{
    char Character { get; }
}




