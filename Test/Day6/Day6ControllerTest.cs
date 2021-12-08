using AOC21.Shared.Day6;
using AOC21.Shared.Parse;
using System.Threading.Tasks;
using Xunit;

namespace Test.Day6
{
    public class Day6ControllerTest
    {
        [Fact]
        public async Task Run_Part1()
        {
            string path = "Test.Day6.Input.txt";
            var input = await ParseHelper.GetInput(path);

            var result = new Day6Controller().Run(256, input);

            Assert.Equal(26984457539, result);
        }
    }
}
