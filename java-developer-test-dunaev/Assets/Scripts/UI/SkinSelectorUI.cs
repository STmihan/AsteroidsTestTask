using Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SkinSelectorUI : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _playButton;


        public void Init(UnityAction nextSkinAction, UnityAction prevSkinAction, UnityAction playAction)
        {
            _nextButton.onClick.AddListener(nextSkinAction);
            _prevButton.onClick.AddListener(prevSkinAction);
            _playButton.onClick.AddListener(playAction);
        }
    }
}