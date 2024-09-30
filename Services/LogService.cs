using Serilog;

namespace Mvc.Services
{
    public class LogService : ILogService
    {
        public void LogInformation(string template, params object[] propertyValues)
        {
            Log.Information(template, propertyValues);
        }

        public void LogError(string template, params object[] propertyValues)
        {
            Log.Error(template, propertyValues);
        }

        public void LogWarning(string template, params object[] propertyValues)
        {
            Log.Warning(template, propertyValues);
        }
    }
}