using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Screen
{
    public abstract class ScreenBase : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screenGroup;

        [Inject]
        public void InjectDependencies(ScreenManager screenManager)
        {
            screenManager.AddScreen(this);
        }

        public void SwitchScreen(bool isEnabled, float duration = 0)
        {
            if (isEnabled)
            {
                EnableScreen(duration);
            }
            else
            {
                StartCoroutine(DisableScreen(duration));
            }
        }

        private void EnableScreen(float duration)
        {
            gameObject.SetActive(true);
            _screenGroup.DOFade(1, duration);
        }

        private IEnumerator DisableScreen(float duration)
        {
            _screenGroup.DOFade(0, duration);
            yield return new WaitForSeconds(duration);
            gameObject.SetActive(false);
        }
    }
}