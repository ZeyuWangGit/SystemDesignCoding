namespace Coding.CodeDesignTask.Router;

public class RouterTests
{
    [Fact]
    public void TestExactMatch()
    {
        IRouter router = new Router();
        router.AddRoute("/bar", "result");
        Assert.Equal("result", router.CallRoute("/bar"));
    }

    [Fact]
    public void TestNestedRoute()
    {
        IRouter router = new Router();
        router.AddRoute("/foo/bar", "result2");
        Assert.Equal("result2", router.CallRoute("/foo/bar"));
    }

    [Fact]
    public void TestWildcardMatch()
    {
        IRouter router = new Router();
        router.AddRoute("/bar/*/baz", "bar");
        Assert.Equal("bar", router.CallRoute("/bar/a/baz"));
    }

    [Fact]
    public void TestWildcardPriority()
    {
        IRouter router = new Router();
        router.AddRoute("/foo/baz", "foo");
        router.AddRoute("/foo/*", "bar");

        Assert.Equal("foo", router.CallRoute("/foo/baz")); // 精确匹配优先
        Assert.Equal("bar", router.CallRoute("/foo/xyz")); // 走通配符
    }

    [Fact]
    public void TestNoMatch()
    {
        IRouter router = new Router();
        router.AddRoute("/foo", "foo");
        Assert.Null(router.CallRoute("/bar"));
    }
}
