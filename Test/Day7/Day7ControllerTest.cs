using AOC21.Shared.Day7;
using AOC21.Shared.Parse;
using System.Threading.Tasks;
using Xunit;

namespace Test.Day7
{
    public class Day7ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day7.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day7Controller().Run(input);

            Assert.Equal(37, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day7.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day7Controller().Run2(input);

            Assert.Equal(168, result);
        }
    }
}
