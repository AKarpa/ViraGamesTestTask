using System.Collections;
using Analytics;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.ObjectMover;
using Services.WindowService;
using StaticData;
using UnityEngine;
using Utils;

namespace Logic
{
    public class LevelPlayingTimer : MonoBehaviour
    {
        private IGameFactory _factory;
        private IStaticDataService _staticData;
        private IWindowService _windowService;
        private IObjectMover _objectMover;
        private IFirebaseAnalyticsService _firebaseAnalyticsService;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _windowService = AllServices.Container.Single<IWindowService>();
            _objectMover = AllServices.Container.Single<IObjectMover>();
            _firebaseAnalyticsService = AllServices.Container.Single<IFirebaseAnalyticsService>();
        }

        public void StartLevelPlayingCoroutine()
        {
            float timeToPlay = _staticData.ForLevel(PlayerPrefsUtils.GetLevelData()).TimeToPlay;
            StartCoroutine(LevelTimePlaying(timeToPlay));
        }

        private IEnumerator LevelTimePlaying(float waitTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            FinishGame();
        }

        private void FinishGame()
        {
            FinishLine finishLine = _factory.CreateFinishLine(new Vector3(0, 4.08f, 75f));
            finishLine.InitFinishLine(_windowService, _objectMover, _firebaseAnalyticsService);
        }
    }
}