namespace Coding.Practice;

/*
 * 给定一个列表 accounts，每个元素 accounts[i] 是一个字符串列表，其中第一个元素 accounts[i][0] 是 名称 (name)，其余元素是 emails 表示该账户的邮箱地址。
   现在，我们想合并这些账户。如果两个账户都有一些共同的邮箱地址，则两个账户必定属于同一个人。请注意，即使两个账户具有相同的名称，它们也可能属于不同的人，因为人们可能具有相同的名称。一个人最初可以拥有任意数量的账户，但其所有账户都具有相同的名称。
   合并账户后，按以下格式返回账户：每个账户的第一个元素是名称，其余元素是 按字符 ASCII 顺序排列 的邮箱地址。账户本身可以以 任意顺序 返回。
   
   示例 1：
   输入：accounts = [["John", "johnsmith@mail.com", "john00@mail.com"], ["John", "johnnybravo@mail.com"], ["John", "johnsmith@mail.com", "john_newyork@mail.com"], ["Mary", "mary@mail.com"]]
   输出：[["John", 'john00@mail.com', 'john_newyork@mail.com', 'johnsmith@mail.com'],  ["John", "johnnybravo@mail.com"], ["Mary", "mary@mail.com"]]
   解释：
   第一个和第三个 John 是同一个人，因为他们有共同的邮箱地址 "johnsmith@mail.com"。 
   第二个 John 和 Mary 是不同的人，因为他们的邮箱地址没有被其他帐户使用。
   可以以任何顺序返回这些列表，例如答案 [['Mary'，'mary@mail.com']，['John'，'johnnybravo@mail.com']，
   ['John'，'john00@mail.com'，'john_newyork@mail.com'，'johnsmith@mail.com']] 也是正确的。
 */
public class AccountsMerge
{
    public IList<IList<string>> MergeAccounts(IList<IList<string>> accounts)
    {
        var emailToIndexDict = new Dictionary<string, int>();
        var emailToNameDict = new Dictionary<string, string>();
        var index = 0;

        foreach (var account in accounts)
        {
            var name = account[0];
            for (int i = 1; i < account.Count; i++)
            {
                var email = account[i];
                if (!emailToIndexDict.ContainsKey(email))
                {
                    emailToIndexDict.Add(email, index++);
                    emailToNameDict.Add(email, name);
                }
            }
        }

        var uf = new UnionFound(index);
        foreach (var account in accounts)
        {
            for (int i = 2; i < account.Count; i++)
            {
                uf.Union(emailToIndexDict[account[1]], emailToIndexDict[account[i]]);
            }
        }

        var indexToGroup = new Dictionary<int, List<string>> ();
        foreach (var email in emailToIndexDict.Keys)
        {
            var rootIndex = uf.Find(emailToIndexDict[email]);
            if (!indexToGroup.ContainsKey(rootIndex))
            {
                indexToGroup.Add(rootIndex, new List<string>());
            }
            indexToGroup[rootIndex].Add(email);
        }
        var res = new List<IList<string>>();
        foreach (var emailGroup in indexToGroup.Values)
        {
            emailGroup.Sort(StringComparer.Ordinal);
            var name = emailToNameDict[emailGroup[0]];
            var account = new List<string>();
            account.Add(name);
            account.AddRange(emailGroup);
            res.Add(account);
        }

        return res;
    }
}

public class UnionFound
{
    private Dictionary<int, int> parent = new Dictionary<int, int>();

    public UnionFound(int n)
    {
        for (int i = 0; i < n; i++)
        {
            parent.Add(i, i);
        }
    }

    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }

    public void Union(int x, int y)
    {
        var rootX = Find(x);
        var rootY = Find(y);
        if (rootX != rootY)
        {
            parent[rootY] = rootX;
        }
    }
}

