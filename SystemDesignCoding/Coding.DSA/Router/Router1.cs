namespace Coding.DSA.Router;

public class Router1:IRouter
{
    private TrieNode _root;

    public Router1()
    {
        _root = new TrieNode();
    }
    
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
        return DFS(parts, 0, _root);
    }

    public string? DFS(string[] parts, int index, TrieNode node)
    {
        if (index == parts.Length)
        {
            return node.Function;
        }

        var part = parts[index];
        if (node.Children.ContainsKey(part))
        {
            var res = DFS(parts, index + 1, node.Children[part]);
            if (res != null)
            {
                return res;
            }
        }

        if (node.Children.ContainsKey("*"))
        {
            var res = DFS(parts, index + 1, node.Children["*"]);
            if (res != null)
            {
                return res;
            }
        }

        return null;

    }
}