using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Configs/WindowStaticData", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        [SerializeField] private List<WindowConfig> configs;
        
        public IEnumerable<WindowConfig> Configs => configs;
    }
}