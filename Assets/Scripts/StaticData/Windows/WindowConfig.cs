using System;
using Windows;
using Services.WindowService;
using UnityEngine;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private WindowID windowID;
        [SerializeField] private WindowBase prefab;
        
        public WindowID WindowID => windowID;
        public WindowBase Prefab => prefab;
    }
}