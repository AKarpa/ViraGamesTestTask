using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private int levelIdKey;
        [SerializeField] private float timeToPlay;

        [Header("Enemy Settings")]
        [SerializeField] private int enemySpotsAmount;
        [SerializeField] private Vector2Int enemyAmountBounds;

        [Header("Upgrade Wall Settings")]
        [SerializeField] private int upgradeWallAmount;
        [SerializeField] private Vector2Int upgradePlusAmountBounds;
        [SerializeField] private Vector2Int upgradeMultiplyAmountBounds;
        
        public int LevelIdKey => levelIdKey;
        public float TimeToPlay => timeToPlay;
        
        public int EnemySpotsAmount => enemySpotsAmount;
        public Vector2Int EnemyAmountBounds => enemyAmountBounds;
        
        public int UpgradeWallAmount => upgradeWallAmount;
        public Vector2Int UpgradePlusAmountBounds => upgradePlusAmountBounds;
        public Vector2Int UpgradeMultiplyAmountBounds => upgradeMultiplyAmountBounds;
    }
}