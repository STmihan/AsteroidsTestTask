using Units;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UI
    {
        private Player _player;
        private SkinSelectorUI _skinSelectorUI;
        public UnityAction PlayCallback;

        public UI(Player player)
        {
            _player = player;
        }

        public void Init()
        {
            _skinSelectorUI = Object.FindObjectOfType<SkinSelectorUI>();
            // _skinSelectorUI.Init();
        }
    }
}