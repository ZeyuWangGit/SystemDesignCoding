namespace Coding.Practice;

/*
 * Trie（发音类似 "try"）或者说 前缀树 是一种树形数据结构，用于高效地存储和检索字符串数据集中的键。这一数据结构有相当多的应用情景，例如自动补全和拼写检查。

   请你实现 Trie 类：

   Trie() 初始化前缀树对象。
   void insert(String word) 向前缀树中插入字符串 word 。
   boolean search(String word) 如果字符串 word 在前缀树中，返回 true（即，在检索之前已经插入）；否则，返回 false 。
   boolean startsWith(String prefix) 如果之前已经插入的字符串 word 的前缀之一为 prefix ，返回 true ；否则，返回 false 。

 */

public class Trie
{
    private readonly TrieNode _root = new TrieNode();
    
    public Trie()
    {
    }

    public void Insert(string word)
    {
        var node = _root;
        foreach (var character in word)
        {
            if (!node.Children.ContainsKey(character))
            {
                node.Children.Add(character, new TrieNode());
            }

            node = node.Children[character];
        }

        node.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        var node = _root;
        foreach (var character in word)
        {
            if (!node.Children.TryGetValue(character, value: out var child))
            {
                return false;
            }

            node = child;
        }

        return node.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        var node = _root;
        foreach (var character in prefix)
        {
            if (!node.Children.TryGetValue(character, out var child))
            {
                return false;
            }

            node = child;
        }

        return true;
    }
}

public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
    public bool IsEndOfWord { get; set; }
}