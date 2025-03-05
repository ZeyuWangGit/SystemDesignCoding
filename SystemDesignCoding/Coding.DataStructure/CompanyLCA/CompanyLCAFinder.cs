namespace Coding.DataStructure.CompanyLCA;

public class CompanyLCAFinder
{
    private readonly CompanyNode _root;
    private readonly HashSet<string> _allEmployeeNames = new();

    public CompanyLCAFinder()
    {
        _root = new CompanyNode("Company");
        var deptA = new CompanyNode("DeptA");
        var deptB = new CompanyNode("DeptB");
        var deptC = new CompanyNode("DeptC");
        
        var emp1 = new CompanyNode("Emp1");
        _allEmployeeNames.Add("Emp1");
        var emp2 = new CompanyNode("Emp2");
        _allEmployeeNames.Add("Emp2");
        var emp3 = new CompanyNode("Emp3");
        _allEmployeeNames.Add("Emp3");
        var emp4 = new CompanyNode("Emp4");
        _allEmployeeNames.Add("Emp4");
        var emp5 = new CompanyNode("Emp5");
        _allEmployeeNames.Add("Emp5");
        
        _root.Children.Add(deptA);
        _root.Children.Add(deptB);
        deptA.Children.Add(emp1);
        deptA.Children.Add(emp2);
        deptB.Children.Add(deptC);
        deptB.Children.Add(emp5);
        deptC.Children.Add(emp3);
        deptC.Children.Add(emp4);
    }

    public string FindLowestCommonAncestor(List<string> employees)
    {
        if (employees == null || employees.Count == 0)
        {
            throw new ArgumentException("Employee list cannot be empty.");
        }

        if (employees.Any(e => !_allEmployeeNames.Contains(e)))
        {
            throw new ArgumentException("One or more employees are not part of the company.");
        }
        
        var res = FindLcaRecursive(_root, employees);
        return res == null ? _root.Name : res.Name;
    }

    private CompanyNode? FindLcaRecursive(CompanyNode? node, List<string> employees)
    {
        if (node == null)
        {
            return null;
        }

        if (employees.Contains(node.Name))
        {
            return node;
        }

        CompanyNode? found = null;
        int count = 0;
        foreach (var child in node.Children)
        {
            var res = FindLcaRecursive(child, employees);
            if (res != null)
            {
                count++;
                if (count > 1) return node;
                found = res;
            }
        }

        return found;
    }
    
}

public class CompanyNode(string name)
{
    public string Name { get; set; } = name;
    public List<CompanyNode> Children { get; set; } = [];
}