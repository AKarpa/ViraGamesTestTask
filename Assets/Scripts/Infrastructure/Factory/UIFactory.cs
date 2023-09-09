using Windows;
using Analytics;
using Services;
using Services.ObjectMover;
using Services.WindowService;
using StaticData;
using StaticData.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootTag = "UIRoot";

        private readonly IStaticDataService _staticData;
        private readonly IObjectMover _objectMover;
        private readonly IResetGameService _resetGameService;
        private readonly IFirebaseAnalyticsService _firebaseAnalyticsService;

        private Transform _uiRoot;

        public UIFactory(IStaticDataService staticDataService, IObjectMover objectMover,
            IResetGameService resetGameService, IFirebaseAnalyticsService firebaseAnalyticsService)
        {
            _staticData = staticDataService;
            _objectMover = objectMover;
            _resetGameService = resetGameService;
            _firebaseAnalyticsService = firebaseAnalyticsService;
        }

        public void CreateStartScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.StartScreen);
            StartScreen startScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<StartScreen>();
            startScreen.InitStartScreen(_objectMover, _firebaseAnalyticsService);
        }

        public void CreateDefeatScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.DefeatScreen);
            DefeatScreen defeatScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<DefeatScreen>();
            defeatScreen.InitDefeatScreen(_resetGameService);
        }

        public void CreateVictoryScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.VictoryScreen);
            VictoryScreen victoryScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<VictoryScreen>();
            victoryScreen.InitDefeatScreen(_resetGameService);
        }

        public void FindRootObject() => _uiRoot = GameObject.FindGameObjectWithTag(UIRootTag).transform;
    }
}