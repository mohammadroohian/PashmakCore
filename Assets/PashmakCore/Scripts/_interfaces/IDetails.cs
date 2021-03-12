
namespace Pashmak.Core
{
    public interface IDetails
    {
        string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams);
    }
}