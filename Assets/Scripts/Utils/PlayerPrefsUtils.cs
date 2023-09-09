using Infrastructure.AssetManagement;
using UnityEngine;

namespace Utils
{
    public static class PlayerPrefsUtils
    {
        public static int GetLevelData()
        {
            int levelData = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);

            if (levelData == 0)
            {
                levelData = 1;
            }

            return levelData;
        }
    }
}