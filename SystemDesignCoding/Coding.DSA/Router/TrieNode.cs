namespace Coding.DSA.Router;

public class TrieNode
{
    public string? Function { get; set; } = null;
    public Dictionary<string, TrieNode> Children { get; set; } = [];
}