namespace Coding.DSA.Router;

public class TrieNode
{
    public string Function { get; set; } = "";
    public Dictionary<string, TrieNode> Children { get; set; } = [];
}