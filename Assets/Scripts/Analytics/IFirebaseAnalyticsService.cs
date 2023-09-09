using Infrastructure.Services;

namespace Analytics
{
    public interface IFirebaseAnalyticsService : IService
    {
        void LogEvent(FirebaseEventsKey eventName, FirebaseEventsKey parameterName, int parameterValue);
    }
}