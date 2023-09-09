using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.CompareObjectListsService;
using Services.ObjectGrouper;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class ObjSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnSpotTransform;
        [SerializeField] private GameObject enemySpawnSpotTransform;

        private IGameFactory _factory;
        private IStaticDataService _staticData;
        private IObjectGrouper _objectGrouper;
        private ICompareObjectListsService _compareService;
        private IObjectMover _objectMover;

        private LevelStaticData _levelData;
        private List<UpgradeWall.UpgradeWall> _upgradeWallPool = new();
        private List<EnemySpot> _enemySpotPool = new();

        private void Awake()
        {
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _factory = AllServices.Container.Single<IGameFactory>();
            _objectGrouper = AllServices.Container.Single<IObjectGrouper>();
            _compareService = AllServices.Container.Single<ICompareObjectListsService>();
            _objectMover = AllServices.Container.Single<IObjectMover>();

            int levelData = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);

            if (levelData == 0)
            {
                levelData = 1;
            }

            _levelData = _staticData.ForLevel(levelData);
            _upgradeWallPool = SpawnUpgradeWall();
            _enemySpotPool = SpawnEnemySpot();

            StartCoroutine(SpawnUpgradeWallCoroutine());
            StartCoroutine(SpawnEnemySpotCoroutine());
        }

        private List<UpgradeWall.UpgradeWall> SpawnUpgradeWall()
        {
            List<UpgradeWall.UpgradeWall> newList = new();

            for (int i = 0; i < _levelData.UpgradeWallAmount; i++)
            {
                UpgradeWall.UpgradeWall upgradeWall = _factory.CreateUpgradeWall(spawnSpotTransform);
                upgradeWall.gameObject.SetActive(false);
                newList.Add(upgradeWall);
            }

            return newList;
        }

        private List<EnemySpot> SpawnEnemySpot()
        {
            List<EnemySpot> newList = new();

            for (int i = 0; i < _levelData.EnemySpotsAmount; i++)
            {
                EnemySpot enemySpot = _factory.CreateEnemySpot(enemySpawnSpotTransform);
                enemySpot.gameObject.SetActive(false);
                newList.Add(enemySpot);
            }

            return newList;
        }

        private UpgradeWall.UpgradeWall RequestUpgradeWall()
        {
            foreach (UpgradeWall.UpgradeWall upgradeWall in _upgradeWallPool.Where(upgradeWall =>
                         !upgradeWall.gameObject.activeInHierarchy))
            {
                upgradeWall.transform.position = spawnSpotTransform.transform.position;
                return upgradeWall;
            }

            UpgradeWall.UpgradeWall newWall =
                _factory.CreateUpgradeWall(spawnSpotTransform);
            newWall.gameObject.SetActive(true);
            _upgradeWallPool.Add(newWall);
            return newWall;
        }

        private EnemySpot RequestEnemySpot()
        {
            foreach (EnemySpot enemySpot in _enemySpotPool.Where(enemySpot => !enemySpot.gameObject.activeInHierarchy))
            {
                enemySpot.transform.position = enemySpawnSpotTransform.transform.position;
                return enemySpot;
            }

            EnemySpot newEnemySpot = _factory.CreateEnemySpot(enemySpawnSpotTransform);

            newEnemySpot.gameObject.SetActive(true);
            _enemySpotPool.Add(newEnemySpot);
            return newEnemySpot;
        }

        IEnumerator SpawnUpgradeWallCoroutine()
        {
            while (true)
            {
                UpgradeWall.UpgradeWall wall = RequestUpgradeWall();
                wall.InitUpgradeWall(_levelData, _objectMover);
                wall.gameObject.SetActive(true);
                yield return new WaitForSeconds(7f);
            }
        }

        IEnumerator SpawnEnemySpotCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                EnemySpot enemySpot = RequestEnemySpot();
                enemySpot.InitEnemySpot(_levelData, _factory, _objectGrouper, _compareService, _objectMover);
                enemySpot.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                enemySpot.gameObject.SetActive(true);
            }
        }
    }
}