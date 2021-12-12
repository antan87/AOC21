using System.Threading.Tasks;
using AOC21.Shared.Day8;
using AOC21.Shared.Parse;
using Xunit;

namespace Test.Day8
{
    public class Day8ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day8.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day8Controller().Run(input);

            Assert.Equal(26, result);
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day8.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day8Controller().Run2(input);

            Assert.Equal(61229, result);
        }
    }
}
