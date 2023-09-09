using Analytics;
using Infrastructure.AssetManagement;
using Logic;
using Services.ObjectMover;
using TMPro;
using UnityEngine;
using Utils;

namespace Windows
{
    public class StartScreen : WindowBase
    {
        [SerializeField] private TextMeshProUGUI coinAmountText;
        private LevelPlayingTimer _levelTimePlayingCounter;
        private IFirebaseAnalyticsService _firebaseAnalyticsService;

        public void InitStartScreen(IObjectMover objectMover, IFirebaseAnalyticsService firebaseAnalyticsService)
        {
            _firebaseAnalyticsService = firebaseAnalyticsService;
            closeButton.onClick.AddListener(StartGameClickAction);

            coinAmountText.text = PlayerPrefs.GetInt("Coin").ToString();
            Time.timeScale = 0;

            objectMover.MoveAction(true);
            _levelTimePlayingCounter = FindObjectOfType<LevelPlayingTimer>();
        }

        private void StartGameClickAction()
        {
            Time.timeScale = 1;
            _levelTimePlayingCounter.StartLevelPlayingCoroutine();

            _firebaseAnalyticsService.LogEvent(FirebaseEventsKey.LevelStart, FirebaseEventsKey.LevelNumber,
                PlayerPrefsUtils.GetLevelData());
        }
    }
}