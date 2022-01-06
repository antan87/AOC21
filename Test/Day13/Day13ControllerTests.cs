using System.Threading.Tasks;
using AOC21.Shared.Day13;
using AOC21.Shared.Parse;
using Xunit;

namespace Test.Day13
{
    public class Day13ControllerTests
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day13.Input.txt";

            var input = await ParseHelper.GetInput(path);
            var result = new Day13Controller().Run(input);

            Assert.Equal(795, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day13.Input.txt";

            var input = await ParseHelper.GetInput(path);
            var result = new Day13Controller().Run2(input);

            Assert.Equal(88, result);
        }
    }
}