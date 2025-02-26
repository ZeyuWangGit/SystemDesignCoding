namespace Coding.CodeDesignTask.Router;

public class RouterTrieNode
{
    public string Function { get; set; } = "";
    public Dictionary<string, RouterTrieNode> Children { get; set; } = [];
}