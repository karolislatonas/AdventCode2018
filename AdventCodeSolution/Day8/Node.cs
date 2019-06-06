using System.Collections.Generic;
using System.Linq;

namespace AdventCodeSolution.Day8
{
    public class Node
    {
        private readonly List<int> metadata;

        private readonly List<Node> children;

        public Node() : this(Enumerable.Empty<Node>(), Enumerable.Empty<int>())
        {

        }

        public Node(IEnumerable<int> metadata) : this(Enumerable.Empty<Node>(), metadata)
        {
            
        }

        private Node(IEnumerable<Node> children, IEnumerable<int> metadata)
        {
            this.metadata = metadata.ToList();
            this.children = children.ToList();
        }

        public IReadOnlyList<int> Metadata => metadata;

        public IReadOnlyList<Node> Children => children;

        public int SumOfSelfAndChildrenMetadata() => Metadata.Sum() + Children.Select(c => c.SumOfSelfAndChildrenMetadata()).Sum();

        public int GetRootNodeValue()
        {
            if(children.Count == 0) return metadata.Sum();

            return Metadata
                .Select(m => m <= children.Count ? children[m - 1].GetRootNodeValue() : 0)
                .Sum();
        }



        public void AddNode(Node node)
        {
            children.Add(node);
        }

        public void AddMetadata(int metadataItem)
        {
            metadata.Add(metadataItem);
        }

        public void AddMetadata(IEnumerable<int> metadataItem)
        {
            metadata.AddRange(metadataItem);
        }
    }
}
