namespace Coding.DSA.Router;

public class Router: IRouter
{
    private TrieNode _root = new TrieNode();
    
    public void AddRoute(string path, string function)
    {
        var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        var node = _root;
        foreach (var part in parts)
        {
            if (!node.Children.ContainsKey(part))
            {
                node.Children.Add(part, new TrieNode());
            }
            node = node.Children[part];
        }
        node.Function = function;
    }

    public string? CallRoute(string path)
    {
        var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        return SearchFunction(parts, 0, _root);

    }

    private string? SearchFunction(string[] parts, int index, TrieNode node)
    {
        if (index == parts.Length)
        {
            return node.Function;
        }
        var part = parts[index];
        if (node.Children.ContainsKey(part))
        {
            var match = node.Children[part];
            var res = SearchFunction(parts, index + 1, match);
            if (res != null)
            {
                return res;
            }
        }

        if (node.Children.ContainsKey("*"))
        {
            var match = node.Children["*"];
            var res = SearchFunction(parts, index + 1, match);
            if (res != null)
            {
                return res;
            }
        }

        return null;
    }
}