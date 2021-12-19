using AOC21.Shared.Day11;
using AOC21.Shared.Parse;
using System.Threading.Tasks;
using Xunit;

namespace Test.Day11
{
    public class Day11ControllerTests
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day11.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day11Controller().Run(input);

            Assert.Equal(1656, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day11.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day11Controller().Run2(input);

            Assert.Equal(195, result);
        }
    }
}
