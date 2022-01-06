using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOC21.Shared.Day12;
using AOC21.Shared.Parse;
using Xunit;
using static AOC21.Shared.Day12.Day12Controller;

namespace Test.Day12
{
    public class Day12ControllerTests
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day12.Input.txt";

            var input = await ParseHelper.GetInput(path);
            List<Node> result = new Day12Controller().Run(input);

            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task Run_Part2()
        {
            string path = "Test.Day12.Input.txt";

            var input = await ParseHelper.GetInput(path);
            List<Node> result = new Day12Controller().Run2(input);

            Assert.Equal(36, result.Count());
        }
    }
}
