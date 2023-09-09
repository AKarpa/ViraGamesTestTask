using Enemy;
using Infrastructure.Services;
using Logic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Player.Player CreatePlayerSpot(GameObject at);
        EnemySpot CreateEnemySpot(GameObject at);
        GameObject CreatePlayerObject(GameObject at);
        GameObject CreateEnemyObject(GameObject at);
        UpgradeWall.UpgradeWall CreateUpgradeWall(GameObject at);
        FinishLine CreateFinishLine(Vector3 at);
    }
}