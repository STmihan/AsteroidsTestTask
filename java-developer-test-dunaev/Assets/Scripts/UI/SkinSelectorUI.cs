using UnityEngine;

namespace UI
{
    public class SkinSelectorUI : MonoBehaviour
    {
        public Canvas Canvas { get; private set; }

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }
    }
}