using Enemy;
using Infrastructure.AssetManagement;
using Logic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public Player.Player CreatePlayerSpot(GameObject at) =>
            _assets.Instantiate<Player.Player>(path: AssetPath.PlayerSpotPath, at: at.transform.position);

        public EnemySpot CreateEnemySpot(GameObject at) =>
            _assets.Instantiate<EnemySpot>(path: AssetPath.EnemySpotPath, at: at.transform.position);

        public GameObject CreatePlayerObject(GameObject at) =>
            _assets.Instantiate<GameObject>(path: AssetPath.PlayerObject, at.transform);

        public GameObject CreateEnemyObject(GameObject at) =>
            _assets.Instantiate<GameObject>(path: AssetPath.EnemyObject, at.transform);

        public UpgradeWall.UpgradeWall CreateUpgradeWall(GameObject at) =>
            _assets.Instantiate<UpgradeWall.UpgradeWall>(path: AssetPath.UpgradeWallPath, at.transform.position);

        public FinishLine CreateFinishLine(Vector3 at) =>
            _assets.Instantiate<FinishLine>(AssetPath.FinishLinePath, at);
    }
}