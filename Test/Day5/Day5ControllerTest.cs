using AOC21.Shared.Day5;
using AOC21.Shared.Parse;
using System.Threading.Tasks;
using Xunit;

namespace Test.Day5
{
    public  class Day5ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
           string path = "Test.Day5.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day5Controller().Run(input);

            Assert.Equal(5, result.part1);
        }
    }
}
