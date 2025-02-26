namespace Coding.CodeDesignTask.Router;

public interface IRouter
{
    void AddRoute(string path, string result);
    string? CallRoute(string path);
}