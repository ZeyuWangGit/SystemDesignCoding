namespace Coding.DSA.CompanyLCA;

public class CompanyLCATests
{
    [Fact]
    public void FindCompanyLCA_ShouldWork_1()
    {
        var companyLca = new CompanyLCA();
        companyLca.BuildCompany();
        Assert.Equal("DeptA", companyLca.FindLowestCommonAncester(["Emp1", "Emp2"]));
        Assert.Equal("DeptB", companyLca.FindLowestCommonAncester(["Emp3", "Emp5"]));
        Assert.Equal("Company", companyLca.FindLowestCommonAncester(["Emp1", "Emp4"]));
    }
}