using Analytics;
using Infrastructure.AssetManagement;
using Services.ObjectMover;
using Services.WindowService;
using UnityEngine;
using Utils;

namespace Logic
{
    public class FinishLine : MonoBehaviour
    {
        private IWindowService _windowService;
        private IObjectMover _objectMover;
        private IFirebaseAnalyticsService _firebaseAnalyticsService;

        public void InitFinishLine(IWindowService windowService, IObjectMover objectMover,
            IFirebaseAnalyticsService firebaseAnalyticsService)
        {
            _firebaseAnalyticsService = firebaseAnalyticsService;
            _objectMover = objectMover;
            _windowService = windowService;
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.back);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player _)) return;
            _windowService.Open(WindowID.VictoryScreen);

            int coinReward = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinKey) + 10;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CoinKey, coinReward);

            int currentLevel = PlayerPrefsUtils.GetLevelData();

            _firebaseAnalyticsService.LogEvent(FirebaseEventsKey.LevelComplete, FirebaseEventsKey.LevelNumber,
                currentLevel);

            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelKey, currentLevel + 1);

            Invoke(nameof(StopLevelMovement), 2f);
        }

        private void StopLevelMovement()
        {
            _objectMover.MoveAction(false);
        }
    }
}