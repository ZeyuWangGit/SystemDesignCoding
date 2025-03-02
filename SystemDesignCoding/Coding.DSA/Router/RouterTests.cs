namespace Coding.DSA.Router;

public class RouterTests
{
    [Fact]
    public void Router_ShouldWorkForExactMatches()
    {
        var router = new Router();
        router.AddRoute("/bar", "result");
        Assert.Equal("result", router.CallRoute("/bar"));
    }
    
    [Fact]
    public void Router_ShouldWorkForWildcard()
    {
        var router = new Router();
        router.AddRoute("/bar/*/baz", "result");
        Assert.Equal("result", router.CallRoute("/bar/a/baz"));
    }
    
    [Fact]
    public void Router_ShouldWorkForWildcard2()
    {
        var router = new Router();
        router.AddRoute("/bar/a/baz/*", "result");
        Assert.Equal("result", router.CallRoute("/bar/a/baz/b"));
    }
    
    [Fact]
    public void Router_ShouldWorkForNotFound()
    {
        var router = new Router();
        router.AddRoute("/bar/a/baz", "result");
        Assert.Null(router.CallRoute("/bar/b/baz"));
    }
}