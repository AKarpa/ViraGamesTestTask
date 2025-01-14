using System.Collections.Generic;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace UpgradeWall
{
    public class UpgradeWall : MonoBehaviour
    {
        [SerializeField] private List<WallTrigger> wallTriggers;
        private IObjectMover _objectMover;


        public void InitUpgradeWall(LevelStaticData levelStaticData, IObjectMover objectMover)
        {
            _objectMover = objectMover;

            SetupWallTriggers(levelStaticData);
            EnableTriggers();
            Invoke(nameof(Hide), 14f);
        }

        private void SetupWallTriggers(LevelStaticData levelStaticData)
        {
            int randomPlusValue = Random.Range(levelStaticData.UpgradePlusAmountBounds.x,
                levelStaticData.UpgradePlusAmountBounds.y + 1);
            int randomMultiplyValue = Random.Range(levelStaticData.UpgradeMultiplyAmountBounds.x,
                levelStaticData.UpgradeMultiplyAmountBounds.y + 1);
            int resultPlusValue = Mathf.RoundToInt(randomPlusValue / 5.0f) * 5;

            foreach (WallTrigger wall in wallTriggers)
            {
                int randomValue = Random.Range(0, 2);
                wall
                    .InitTrigger(randomValue == 0 ? WallType.PlusWall : WallType.MultiplyWall,
                        randomValue == 0 ? resultPlusValue : randomMultiplyValue,
                        randomValue == 0 ? "+" + resultPlusValue : "x" + randomMultiplyValue,
                        DisableTriggers);
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.back);
        }

        private void EnableTriggers()
        {
            foreach (WallTrigger trigger in wallTriggers)
            {
                trigger.GetComponent<Collider>().enabled = true;
            }
        }

        private void DisableTriggers()
        {
            foreach (WallTrigger trigger in wallTriggers)
            {
                trigger.GetComponent<Collider>().enabled = false;
            }
        }
    }
}