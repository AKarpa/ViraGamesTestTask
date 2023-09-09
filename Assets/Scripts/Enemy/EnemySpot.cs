using System.Collections.Generic;
using Infrastructure.Factory;
using Logic;
using Services.CompareObjectListsService;
using Services.ObjectGrouper;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class EnemySpot : MonoBehaviour
    {
        [SerializeField] private EnemySpotTrigger enemySpotTrigger;
        [SerializeField] private Counter counter;

        private LevelStaticData _levelStaticData;
        private Vector3 _fightTarget;
        private bool _isFighting;

        private IGameFactory _gameFactory;
        private IObjectGrouper _objectGrouper;
        private ICompareObjectListsService _compareObjectListsService;
        private IObjectMover _objectMover;
        
        public List<Transform> EnemySpotObjects { get; } = new();

        public void InitEnemySpot(LevelStaticData levelStaticData, IGameFactory gameFactory,
            IObjectGrouper objectGrouper, ICompareObjectListsService compareObjectListsService,
            IObjectMover objectMover)
        {
            _objectMover = objectMover;
            _compareObjectListsService = compareObjectListsService;
            _objectGrouper = objectGrouper;
            _levelStaticData = levelStaticData;
            _gameFactory = gameFactory;

            GenerateEnemySpotObjects();
            enemySpotTrigger.InitEnemySpotTrigger(this, _compareObjectListsService);

            UpdateEnemySpotCounter(EnemySpotObjects.Count);
            _isFighting = false;
        }

        public void SetupFightTarget(Vector3 fightTarget)
        {
            _fightTarget = fightTarget;
            _isFighting = true;
        }

        public void UpdateEnemySpotCounter(int newAmount)
        {
            counter.ChangeCounterValue(newAmount);
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.forward);

            if (_isFighting)
            {
                _objectMover.MoveTowardsToObject(transform, _fightTarget);
            }
        }

        private void GenerateEnemySpotObjects()
        {
            int randomEnemyAmount = Random.Range(_levelStaticData.EnemyAmountBounds.x,
                _levelStaticData.EnemyAmountBounds.y + 1);

            int diffAmount = EnemySpotObjects.Count - randomEnemyAmount;
            if (diffAmount < 0)
                diffAmount = -(diffAmount);

            for (int i = 0; i < diffAmount; i++)
            {
                GameObject newEnemyObj = _gameFactory.CreateEnemyObject(gameObject);
                newEnemyObj.SetActive(false);
                EnemySpotObjects.Add(newEnemyObj.transform);
            }

            ActivateEnemies(randomEnemyAmount);
        }

        private void ActivateEnemies(int randomEnemyAmount)
        {
            if (randomEnemyAmount > EnemySpotObjects.Count) return;

            for (int i = 0; i < randomEnemyAmount; i++)
            {
                EnemySpotObjects[i].gameObject.SetActive(true);
            }

            _objectGrouper.GroupObjects(EnemySpotObjects, .5f);
        }
    }
}