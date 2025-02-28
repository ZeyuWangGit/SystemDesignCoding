namespace Coding.Practice;

/*
 * 请你设计一个数据结构，支持 添加新单词 和 查找字符串是否与任何先前添加的字符串匹配 。
   
   实现词典类 WordDictionary ：
   
   WordDictionary() 初始化词典对象
   void addWord(word) 将 word 添加到数据结构中，之后可以对它进行匹配
   bool search(word) 如果数据结构中存在字符串与 word 匹配，则返回 true ；否则，返回  false 。word 中可能包含一些 '.' ，每个 . 都可以表示任何一个字母。
   
   输入：
   ["WordDictionary","addWord","addWord","addWord","search","search","search","search"]
   [[],["bad"],["dad"],["mad"],["pad"],["bad"],[".ad"],["b.."]]
   输出：
   [null,null,null,null,false,true,true,true]
   
   解释：
   WordDictionary wordDictionary = new WordDictionary();
   wordDictionary.addWord("bad");
   wordDictionary.addWord("dad");
   wordDictionary.addWord("mad");
   wordDictionary.search("pad"); // 返回 False
   wordDictionary.search("bad"); // 返回 True
   wordDictionary.search(".ad"); // 返回 True
   wordDictionary.search("b.."); // 返回 True
 */
public class WordDictionary
{
    private readonly TrieNode _root = new TrieNode();
    
    public WordDictionary() {
        
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
    
    public bool Search(string word)
    {
        var node = _root;
        return DFS(word, 0, node);
    }

    private bool DFS(string word, int index, TrieNode node)
    {
        if (index == word.Length)
        {
            return node.IsEndOfWord;
        }

        var ch = word[index];
        if (ch == '.')
        {
            foreach (var (_, child) in node.Children)
            {
                if (DFS(word, index + 1, child))
                {
                    return true;
                }
            }
        }
        else
        {
            if (node.Children.ContainsKey(ch))
            {
                var child = node.Children[ch];
                return DFS(word, index + 1, child);
            }
        }

        return false;
    }
}
