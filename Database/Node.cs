using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Index(nameof(Name))]
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? TreeId { get; set; }
        public Node? Tree { get; set; }

        public int? ParentNodeId { get; set; }
        public Node? ParentNode { get; set; }

        public ICollection<Node> TreeNodes { get; set; } = new List<Node>();
        public ICollection<Node> Children { get; set; } = new List<Node>();
    }
}
