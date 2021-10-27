using Units;
using UnityEngine;
using UnityEngine.UI;
using static Assets.SOAssets;

namespace UI
{
    public class SkinSelector : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _playButton;

        private Player _player;
        private int _skinNumber1;

        private int _skinNumber
        {
            get => _skinNumber1;
            set
            {
                if (value > SO.assets.playerShips.Length-1) value = 0;
                if (value < 0) value = SO.assets.playerShips.Length - 1;
                _skinNumber1 = value;
            }
        }

        private void Awake()
        {
            _skinNumber1 = 0;
            _player = FindObjectOfType<Player>();
            _nextButton.onClick.AddListener(() => _player.SetPlayerSprite(SO.assets.playerShips[++_skinNumber]));
            _prevButton.onClick.AddListener(() => _player.SetPlayerSprite(SO.assets.playerShips[--_skinNumber]));
            _playButton.onClick.AddListener(Play);
        }

        private void Play()
        {
            print("Playing!");
        }
    }
}