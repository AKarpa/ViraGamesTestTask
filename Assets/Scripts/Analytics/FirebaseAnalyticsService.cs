using System.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace Analytics
{
    public class FirebaseAnalyticsService : IFirebaseAnalyticsService
    {
        private FirebaseApp _firebaseApp;

        public FirebaseAnalyticsService()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(delegate(Task<DependencyStatus> task)
            {
                DependencyStatus dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _firebaseApp = FirebaseApp.DefaultInstance;
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        public void LogEvent(FirebaseEventsKey eventName, FirebaseEventsKey parameterName, int parameterValue)
        {
            if (_firebaseApp == null)
            {
                Debug.LogWarning(
                    "Couldn't use firebase because it haven't initialized yet" +
                    " or it could resolve all dependencies");
                return;
            }

            Firebase.Analytics.FirebaseAnalytics
                .LogEvent(eventName.ToString(), parameterName.ToString(), parameterValue);
        }
    }
}