namespace Mvc.Services
{
    public interface ILogService
    {
        void LogInformation(string template, params object[] propertyValues);
        void LogError(string template, params object[] propertyValues);
        void LogWarning(string template, params object[] propertyValues);
    }
}