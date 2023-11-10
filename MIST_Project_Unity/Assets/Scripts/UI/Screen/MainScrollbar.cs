using UnityEngine;
using UnityEngine.UI;

namespace MistProject.UI.Screen
{
    public class MainScrollbar : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scroll;

        public void SetScrollContent(Transform content)
        {
            _scroll.content = (RectTransform) content;
        }
    }
}