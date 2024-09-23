namespace TestNodesWeb.Api.Features.Trees.Models
{
    public class MNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<MNode> Children { get; set; } = new List<MNode>();
    }
}