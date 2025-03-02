namespace Coding.DSA.Router;

public interface IRouter
{
    void AddRoute(string path, string function);
    string? CallRoute(string path);
}