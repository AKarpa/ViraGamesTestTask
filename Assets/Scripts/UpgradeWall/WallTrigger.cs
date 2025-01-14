﻿using System;
using TMPro;
using UnityEngine;

namespace UpgradeWall
{
    public class WallTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject hologram;
        [SerializeField] private WallType wallType;
        [SerializeField] private TextMeshPro valueText;
        private int _triggerValue;
        private Action _triggerAction;

        public void InitTrigger(WallType wallTypeValue, int value, string valueString, Action triggerAction = null)
        {
            if (value <= 0) return;
            wallType = wallTypeValue;
            _triggerValue = value;
            valueText.text = valueString;

            _triggerAction = triggerAction;
            hologram.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            _triggerAction?.Invoke();
            hologram.gameObject.SetActive(false);
            switch (wallType)
            {
                case WallType.PlusWall:
                    player.PlayerObjectSpawner.SpawnPlayerObject(_triggerValue);
                    break;
                case WallType.MultiplyWall:
                    int multiplyValue = player.PlayerObjects.Count * _triggerValue;
                    player.PlayerObjectSpawner.SpawnPlayerObject(multiplyValue);
                    break;
            }
        }
    }
}