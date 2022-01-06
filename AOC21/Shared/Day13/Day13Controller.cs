using System.Diagnostics;
using AOC21.Shared.Algebra.Models;
using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day13
{
    public class Day13Controller
    {
        public int Run(string input)
        {
         var origami   = ParseHelper.Parse(new PaperParser(), input);

            origami.TryFoldPaper(out origami);


      

            return origami.Points.Count();
        }

        public int Run2(string input)
        {
            var origami = ParseHelper.Parse(new PaperParser(), input);

            while(origami.TryFoldPaper(out origami));

            int maxY = origami.Points.Max(item => item.Y);
            int maxX = origami.Points.Max(item => item.X);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (origami.Points.Any(item => item.X == x && item.Y == y))
                        Debug.Write('*');
                    else
                        Debug.Write(" ");

                }

                Debug.WriteLine("");
            }

            return origami.Points.Count();
        }

        private class PaperParser : IParser<Origami>
        {
            public Origami Parse(string value)
            {
                var entries = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, value.Trim(), StringSplitOptions.RemoveEmptyEntries);

                var instructions = entries.Where(s => s.StartsWith('f')).Select(entry =>
               GetInstruction(entry));

                var points = entries.Where(s => !s.StartsWith('f')).Select(entry =>
               GetPoint(entry));


                return new Origami(points.ToList(), new Queue<IFoldInstruction>(instructions));

                 static IFoldInstruction GetInstruction(string entry)
                {
                    var splits = ParseHelper.Parse(new string[] { " " }, ParserCreator.StringParser, entry, StringSplitOptions.RemoveEmptyEntries);
                   var numberSplit = ParseHelper.Parse(new string[] { "=" }, ParserCreator.StringParser, splits.Last(), StringSplitOptions.TrimEntries);
                    int line = Convert.ToInt32(numberSplit.Last().ToString());
                    if (splits.Any(it => it.Contains("y=")))
                        return new FoldInstructionY(line);

                    return new FoldInstructionX(line);
                 }

                static Point2D GetPoint(string entry)
                {
                    var splits = ParseHelper.Parse(new string[] { "," }, ParserCreator.StringParser, entry, StringSplitOptions.TrimEntries);
                    return new Point2D(Convert.ToInt32(splits[0]), Convert.ToInt32(splits[1]));
                }
            } 
        }

        public record Origami(List< Point2D> Points, Queue<IFoldInstruction> Instructions)
        { 
            public bool TryFoldPaper(out Origami origami)
        {
                if(!Instructions.Any())
                {
                    origami = this;
                    return false;
                }

                var instruction = Instructions.Dequeue();

               var newPoints = Fold(Points, instruction);
                origami =  new Origami(newPoints, Instructions);

                return true;
            }

            public  static List<Point2D>  Fold(List<Point2D> points, IFoldInstruction instruction)
            {
                switch(instruction)
                {
                    case FoldInstructionY foldInstructionY:
                        {

                           var foldPoints = points.Where(point => point.Y > foldInstructionY.Line).Select(point => {
                               var newY = foldInstructionY.Line - ( point.Y- foldInstructionY.Line);

                               return new Point2D(point.X,newY);
                               });


                            var excludedPoints = points.Where(point => point.Y <= foldInstructionY.Line).ToList();
                            excludedPoints.AddRange(foldPoints);

                            return excludedPoints.Distinct().ToList();
                        }


                    case FoldInstructionX foldInstructionX:
                        {

                            var foldPoints = points.Where(point => point.X > foldInstructionX.Line).Select(point => {
                                var newX = foldInstructionX.Line - (point.X- foldInstructionX.Line);

                                return new Point2D(newX, point.Y);
                            });


                            var excludedPoints = points.Where(point => point.X < foldInstructionX.Line).ToList();
                            excludedPoints.AddRange(foldPoints);

                            return excludedPoints.Distinct().ToList();
                        }

                    default:
                        throw new NotImplementedException();
                }
            }

        }

        public record Paper(IEnumerable< Point2D> points);

        public interface IFoldInstruction
        {
            int Line { get;  }
        }

        public record FoldInstructionX(int Line) : IFoldInstruction;
        public record FoldInstructionY(int Line) : IFoldInstruction;
    }
}