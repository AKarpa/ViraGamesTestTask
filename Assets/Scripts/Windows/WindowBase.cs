using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;

        private void Awake()
        {
            OnAwake();
        }

        private void OnAwake()
        {
            closeButton.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}