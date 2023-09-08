using Infrastructure.AssetManagement;
using Services.ObjectMover;
using Services.WindowService;
using UnityEngine;

namespace Logic
{
    public class FinishLine : MonoBehaviour
    {
        private IWindowService _windowService;
        private IObjectMover _objectMover;

        public void InitFinishLine(IWindowService windowService, IObjectMover objectMover)
        {
            _objectMover = objectMover;
            _windowService = windowService;
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.back);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out Player.Player player))
            {
                _windowService.Open(WindowID.VictoryScreen);

                int coinReward = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinKey) + 10;
                PlayerPrefs.SetInt(PlayerPrefsKeys.CoinKey, coinReward);

                int currentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);

                if (currentLevel == 0)
                {
                    currentLevel = 1;
                }

                PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelKey, currentLevel + 1);

                Invoke(nameof(StopLevelMovement), 2f);
            }
        }

        private void StopLevelMovement()
        {
            _objectMover.MoveAction(false);
        }
    }
}