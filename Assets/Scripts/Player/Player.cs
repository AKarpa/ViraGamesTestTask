using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services.Input;
using Services.ObjectGrouper;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Counter counter;

        private IInputService _inputService;
        private IObjectGrouper _objectGrouper;
        private PlayerMover _playerMover;
        private IGameFactory _gameFactory;

        public PlayerObjectSpawner PlayerObjectSpawner { get; private set; }
        public SphereCollider SphereCollider { get; private set; }
        public List<Transform> PlayerObjects { get; } = new();

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _objectGrouper = AllServices.Container.Single<IObjectGrouper>();
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            SphereCollider = gameObject.AddComponent<SphereCollider>();

            _playerMover = new PlayerMover(_inputService);
            PlayerObjectSpawner = new PlayerObjectSpawner(this, _gameFactory, _objectGrouper);

            _objectGrouper.GroupObjects(PlayerObjects, .5f);
            _objectGrouper.CalculateGroupColliderSize(PlayerObjects, SphereCollider);

            UpdatePlayerCounterValue(PlayerObjects.Count);
        }

        private void Update()
        {
#if UNITY_EDITOR
            _playerMover.UpdatePosStandalone(transform);
            return;
#endif
            _playerMover.UpdatePosMobile(transform);
        }

        public void UpdatePlayerCounterValue(int newAmount)
        {
            counter.ChangeCounterValue(newAmount);
        }
    }
}