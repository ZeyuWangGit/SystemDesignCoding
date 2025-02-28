namespace Coding.Practice;
/*
 * 设计一个使用单词列表进行初始化的数据结构，单词列表中的单词 互不相同 。 如果给出一个单词，请判定能否只将这个单词中一个字母换成另一个字母，使得所形成的新单词存在于你构建的字典中。

   实现 MagicDictionary 类：

   MagicDictionary() 初始化对象
   void buildDict(String[] dictionary) 使用字符串数组 dictionary 设定该数据结构，dictionary 中的字符串互不相同
   bool search(String searchWord) 给定一个字符串 searchWord ，判定能否只将字符串中一个字母换成另一个字母，使得所形成的新字符串能够与字典中的任一字符串匹配。如果可以，返回 true ；否则，返回 false 。

   示例：

   输入
   ["MagicDictionary", "buildDict", "search", "search", "search", "search"]
   [[], [["hello", "leetcode"]], ["hello"], ["hhllo"], ["hell"], ["leetcoded"]]
   输出
   [null, null, false, true, false, false]

   解释
   MagicDictionary magicDictionary = new MagicDictionary();
   magicDictionary.buildDict(["hello", "leetcode"]);
   magicDictionary.search("hello"); // 返回 False
   magicDictionary.search("hhllo"); // 将第二个 'h' 替换为 'e' 可以匹配 "hello" ，所以返回 True
   magicDictionary.search("hell"); // 返回 False
   magicDictionary.search("leetcoded"); // 返回 False
 */

public class MagicDictionary
{
    private readonly TrieNode _root = new TrieNode();

    public MagicDictionary()
    {
    }

    public void BuildDict(string[] dictionary)
    {
        foreach (var word in dictionary)
        {
            AddWord(word);
        }
    }

    public bool Search(string searchWord)
    {
        var node = _root;
        return DFS(searchWord, 0, node, false);
    }

    private bool DFS(string word, int index, TrieNode node, bool hasUsed)
    {
        if (index == word.Length)
        {
            return hasUsed && node.IsEndOfWord;
        }

        var ch = word[index];
        if (!hasUsed)
        {
            foreach (var (c, child) in node.Children)
            {
                if (c != ch && DFS(word, index + 1, child, true))
                {
                    return true;
                }
            }   
        }

        if (node.Children.TryGetValue(ch, out var child1))
        {
            return DFS(word, index + 1, child1, hasUsed);
        }


        return false;
    }

    public void AddWord(string word)
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
}