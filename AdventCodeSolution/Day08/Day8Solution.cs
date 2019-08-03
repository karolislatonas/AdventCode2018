using System;
using System.Linq;

namespace AdventCodeSolution.Day08
{
    public class Day8Solution
    {
        public static void SolvePartOne()
        {
            var input = GetInput();

            var headNode = ParseNodes(input);

            headNode.SumOfSelfAndChildrenMetadata().WriteLine("Day 8, Part 1: ");
        }

        public static void SolvePartTwo()
        {
            var input = GetInput();

            var headNode = ParseNodes(input);

            headNode.GetRootNodeValue().WriteLine("Day 8, Part 2: ");
        }

        private static Node ParseNodes(int[] input)
        {
            return ParseNodes(input, 0).node;
        }

        private static (Node node, int finishedAt) ParseNodes(int[] input, int currentIndex)
        {
            var childrenCount = input[currentIndex];
            var metadataCount = input[currentIndex + 1];

            if(childrenCount == 0)
            {
                var firstMetadataIndex = currentIndex + 2;

                return (new Node(input.Slice(firstMetadataIndex, metadataCount)), firstMetadataIndex + metadataCount);
            }

            var node = new Node();

            var childStartIndex = currentIndex + 2;
            for(var i = 0; i < childrenCount; i++)
            {
                var childNode = ParseNodes(input, childStartIndex);
                node.AddNode(childNode.node);
                childStartIndex = childNode.finishedAt;
            }

            node.AddMetadata(input.Slice(childStartIndex, metadataCount));

            return (node, childStartIndex + metadataCount);
        }


        private static int[] GetInput() => InputResources.Day8Input.Split(' ').Select(int.Parse).ToArray();

    }
}
