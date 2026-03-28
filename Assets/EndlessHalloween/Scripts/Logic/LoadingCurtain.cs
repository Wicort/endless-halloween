using System.Collections;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain: MonoBehaviour
    {
        public CanvasGroup Curtain;
        private bool _canShow;

        private void Awake()
        {
            _canShow = false;
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeOut());
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_canShow == true)
                yield return null;

            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            gameObject.SetActive(false);
            _canShow = true;
        }
        private IEnumerator FadeOut()
        {
            while (_canShow == false)
                yield return null;

            while (Curtain.alpha < 1)
            {
                Curtain.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            _canShow = false;
        }
    }
}
