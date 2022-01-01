using AOC21.Shared.Parse;
using AOC21.Shared.Parse.Interface;

namespace AOC21.Shared.Day12
{
    public class Day12Controller
    {
        public List<Node> Run(string input)
        {
            NodeStructure nodeStructure = ParseHelper.Parse(new CaveParser(), input);
            List<Node> nodes = new();
            var node = new Node(null, nodeStructure.Step);
            //node.InitializeVisited(
            FindPaths(nodeStructure, node, nodes,1);

            return nodes;

           
        }
        public List<Node> Run2(string input)
        {
            NodeStructure nodeStructure = ParseHelper.Parse(new CaveParser(), input);
            List<Node> nodes = new();
            var node = new Node(null, nodeStructure.Step);
            FindPaths(nodeStructure, node, nodes, 2);

            return nodes;
        }

        static void FindPaths(NodeStructure nodeStructure, Node node, List<Node> nodes, int max)
        {
            if (node.Step is EndStep)
            {
                nodes.Add(node);
                return;
            }

            if (node.Step is SmallCaveStep)
                node.VisitedSteps.Add(node.Step);

            foreach (var step in nodeStructure.Steps)
            {

                if (step.Step is StartStep)
                    continue;

                if (node.IsExceeded(max, step.Step))
                    continue;

                FindPaths(step, new Node(node, step.Step), nodes, max);
            }
        }

        private class CaveParser : IParser<NodeStructure>
        {
            public NodeStructure Parse(string value)
            {
                var steps = ParseHelper.Parse(new string[] { Environment.NewLine }, ParserCreator.StringParser, value, StringSplitOptions.TrimEntries)
                       .Select(part => part.Split('-')).ToList();

                var reversed = steps.Select(st => st.Reverse().ToArray()).ToList();
                steps.AddRange(reversed);

                var group = steps.GroupBy(item => item.First());

                var startSteps = group.First(item => item.Key == "start");
                var start = new NodeStructure(null, new StartStep("start"));
                startSteps.ToList().ForEach(part => start.Add(GetStep(part[1])));

                var dictionary = new Dictionary<IStep, NodeStructure>
                {
                    [start.Step] = start
                };

                Recursive(dictionary, start, group, true);

                static void Recursive(Dictionary<IStep, NodeStructure> dictionary, NodeStructure node, IEnumerable<IGrouping<string, string[]>> steps, bool isFirst = false)
                {
                    if (!isFirst && node.Step is StartStep)
                        return;

                    foreach (var step in node.Steps)
                    {

                        if (step.Step is StartStep)
                            continue;

                        if (step.Step is EndStep)
                            continue;

                        if (dictionary.TryGetValue(step.Step, out NodeStructure? nod) && node != null)
                        {
                            nod.Steps.ForEach(s => step.Steps.Add(s));
                        }
                        else
                        {
                            var currentNodeSteps = steps.FirstOrDefault(item => item.Key == step.Step.Name);
                            if (currentNodeSteps == null)
                                continue;

                            currentNodeSteps.ToList().ForEach(part => step.Add(GetStep(part[1])));
                            dictionary[step.Step] = step;
                            Recursive(dictionary, step, steps);
                        }
                    }
                }

                return start;

                static IStep GetStep(string typeOfStep)
                {
                    if (typeOfStep.Equals("start"))
                        return new StartStep(typeOfStep);

                    if (typeOfStep.Equals("end"))
                        return new EndStep(typeOfStep);

                    if (typeOfStep.All(char.IsUpper))
                        return new BigCaveStep(typeOfStep);

                    return new SmallCaveStep(typeOfStep);
                }
            }
        }

        public interface IStep
        {
            string Name { get; }
        }

        public record StartStep(string Name) : IStep;
        public record EndStep(string Name) : IStep;
        public interface ICave : IStep
        {
        }

        public record BigCaveStep(string Name) : ICave;
        public record SmallCaveStep(string Name) : ICave;

        public record NodeStructure(NodeStructure? Previous, IStep Step)
        {
            public void Add(IStep step)
            {
                this.Steps.Add(new NodeStructure(this, step));
            }

            public List<NodeStructure> Steps { get; } = new();
        }

        public record Node
        {
            public Node(Node? previous, IStep step)
            {
                Previous = previous;
                Step = step;

                if (this.Previous != null)
                    Previous.Next = this;

                if (previous != null)
                {
                    VisitedSteps.AddRange(previous.VisitedSteps);
                }
            }

            public Node? Next { get; private set; }

            public List<IStep> VisitedSteps { get; } = new();

            public bool IsExceeded(int max, IStep step)
            {
                var groups =  this.VisitedSteps.GroupBy(item => item);

                if (groups.Any(item => item.Count() == max) && groups.Any(item => item.Key.Equals(step)))
                    return true;

                return false;
            }

            public Node? Previous { get; }
            public IStep Step { get; }

            public override string ToString() => $"Previous {Previous?.Step} Step {Step} Next {Next?.Step}";
        }
    }
}
