using System.Collections;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return new WaitForSecondsRealtime(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}