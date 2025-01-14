﻿using Infrastructure.Factory;
using Logic;
using Services.WindowService;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IWindowService windowService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _windowService = windowService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            _windowService.Open(WindowID.StartScreen);

            Player.Player playerSpot = _gameFactory.CreatePlayerSpot(GameObject.FindWithTag(InitialPointTag));
            GameObject playerObj = _gameFactory.CreatePlayerObject(playerSpot.gameObject);
            playerSpot.PlayerObjects.Add(playerObj.transform);
            playerSpot.UpdatePlayerCounterValue(1);

            _stateMachine.Enter<GameLoopState>();
        }
    }
}