namespace Coding.CodeDesignTask.Router;

public class Router : IRouter
{
    private readonly RouterTrieNode _root = new RouterTrieNode();
    public void AddRoute(string path, string func)
    {
        var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        var node = _root;
        foreach (var part in parts)
        {
            if (!node.Children.ContainsKey(part))
            {
                node.Children.Add(part, new RouterTrieNode());
            }
            node = node.Children[part];
        }
        node.Function = func;
    }

    public string? CallRoute(string path)
    {
        var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        return Search(_root, parts, 0);
    }

    private string? Search(RouterTrieNode node, string[] parts, int index)
    {
        if (index == parts.Length)
        {
            return node.Function;
        }
        
        var part = parts[index];
        if (node.Children.TryGetValue(part, out var exactMatch))
        {
            var res = Search(exactMatch, parts, index + 1);
            if (res != null)
            {
                return res;
            }
        }

        if (node.Children.TryGetValue("*", out var wildcardMatch))
        {
            var res = Search(wildcardMatch, parts, index + 1);
            if (res != null)
            {
                return res;
            }
        }

        return null;
    }
}