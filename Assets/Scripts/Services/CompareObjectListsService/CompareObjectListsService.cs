﻿using System.Collections;
using System.Collections.Generic;
using Analytics;
using Enemy;
using Infrastructure.AssetManagement;
using Services.ObjectMover;
using Services.WindowService;
using UnityEngine;
using Utils;

namespace Services.CompareObjectListsService
{
    public class CompareObjectListsService : ICompareObjectListsService
    {
        private readonly IObjectMover _objectMover;
        private readonly IWindowService _windowService;
        private readonly IFirebaseAnalyticsService _firebaseAnalyticsService;

        public CompareObjectListsService(IObjectMover objectMover, IWindowService windowService,
            IFirebaseAnalyticsService firebaseAnalyticsService)
        {
            _firebaseAnalyticsService = firebaseAnalyticsService;
            _objectMover = objectMover;
            _windowService = windowService;
        }

        public IEnumerator CompareLists(EnemySpot enemy, Player.Player player)
        {
            _objectMover.MoveAction(false);

            List<Transform> playerList = player.PlayerObjects;
            List<Transform> enemyList = enemy.EnemySpotObjects;

            int playerListCount = playerList.Count;
            int enemyListCount = enemyList.Count;

            float timeBetweenDisable = enemyListCount <= 10 ? 0.1f : .03f;

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (playerListCount <= 0)
                {
                    _windowService.Open(WindowID.DefeatScreen);
                    player.gameObject.SetActive(false);

                    _firebaseAnalyticsService.LogEvent(FirebaseEventsKey.LevelComplete, FirebaseEventsKey.LevelNumber,
                        PlayerPrefsUtils.GetLevelData());

                    yield break;
                }

                playerList[i].gameObject.SetActive(false);
                enemyList[i].gameObject.SetActive(false);

                playerListCount--;
                enemyListCount--;

                enemy.UpdateEnemySpotCounter(enemyListCount);
                player.UpdatePlayerCounterValue(playerListCount);

                yield return new WaitForSeconds(timeBetweenDisable);
            }

            playerList.RemoveRange(0, enemyList.Count);
            enemyList.Clear();
            enemy.gameObject.SetActive(false);

            _objectMover.MoveAction(true);
        }
    }
}