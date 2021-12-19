using AOC21.Shared.Day10;
using AOC21.Shared.Parse;
using System.Threading.Tasks;
using Xunit;

namespace Test.Day10
{
    public class Day10ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day10.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day10Controller().Run(input);

            Assert.Equal(26397, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day10.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day10Controller().Run2(input);

            Assert.Equal(288957, result);
        }
    }
}