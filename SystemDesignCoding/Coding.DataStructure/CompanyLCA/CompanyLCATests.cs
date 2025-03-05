namespace Coding.DataStructure.CompanyLCA;

public class CompanyLcaTests
{
    [Fact]
    public void FindCompanyLCA_ShouldWork_1()
    {
        var companyLca = new CompanyLCAFinder();
        Assert.Equal("DeptA", companyLca.FindLowestCommonAncestor(["Emp1", "Emp2"]));
        Assert.Equal("DeptB", companyLca.FindLowestCommonAncestor(["Emp3", "Emp5"]));
        Assert.Equal("Company", companyLca.FindLowestCommonAncestor(["Emp1", "Emp4"]));
    }
}