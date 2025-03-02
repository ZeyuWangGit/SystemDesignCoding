using Coding.DSA.Router;

namespace Coding.DSA.CompanyLCA;

public class CompanyLCA
{
    private CompanyNode _root = new CompanyNode("Company");

    public void BuildCompany()
    {
        var deptA = new CompanyNode("DeptA");
        var deptB = new CompanyNode("DeptB");
        var deptC = new CompanyNode("DeptC");
        
        var emp1 = new CompanyNode("Emp1");
        var emp2 = new CompanyNode("Emp2");
        var emp3 = new CompanyNode("Emp3");
        var emp4 = new CompanyNode("Emp4");
        var emp5 = new CompanyNode("Emp5");
        
        _root.Children.Add(deptA);
        _root.Children.Add(deptB);
        deptA.Children.Add(emp1);
        deptA.Children.Add(emp2);
        deptB.Children.Add(deptC);
        deptB.Children.Add(emp5);
        deptC.Children.Add(emp3);
        deptC.Children.Add(emp4);
    }

    public string FindLowestCommonAncester(List<string> employees)
    {
        var res = FindCLA(_root, employees);
        return res == null ? _root.Name : res.Name;
    }

    public CompanyNode? FindCLA(CompanyNode? node, List<string> employees)
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
        var count = 0;
        foreach (var child in node.Children)
        {
            var res = FindCLA(child, employees);
            if (res != null)
            {
                found = res;
                count++;
            }
        }

        if (count > 1)
        {
            return node;
        }

        return found;
    }
}

public class CompanyNode
{
    public string Name { get; set; }
    public List<CompanyNode> Children { get; set; } = [];

    public CompanyNode(string name)
    {
        Name = name;
    }
}