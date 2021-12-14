using System.Threading.Tasks;
using AOC21.Shared.Day9;
using AOC21.Shared.Parse;
using Xunit;

namespace Test.Day9
{
    public class Day9ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day9.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day9Controller().Run(input);

            Assert.Equal(15, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day9.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day9Controller().Run2(input);

            Assert.Equal(1134, result);
        }
    }
}
