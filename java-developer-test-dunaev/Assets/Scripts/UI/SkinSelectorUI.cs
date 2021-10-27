using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkinSelectorUI : MonoBehaviour, IScreen
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _playButton;

        private Player _player;
        private Sprite[] _skins;
        private int _skinCounts;
        private int _skinNumber;


        public void SetPlayer(Player player) => _player = player;

        public void SetSprites(ref Sprite[] sprites)
        {
            _skins = sprites;
            _skinCounts = sprites.Length;
        }
        
        public void Init()
        {
            _skinNumber = 0;
            _player.CanBeControl = false;
            
            _nextButton.onClick.AddListener(() => _player.SetPlayerSprite(_skins[IncreasedSkinNumber()]));
            _prevButton.onClick.AddListener(() => _player.SetPlayerSprite(_skins[DecreaseSkinNumber()]));
            _playButton.onClick.AddListener(Play);
        }

        private int IncreasedSkinNumber()
        {
            if (++_skinNumber >= _skinCounts) _skinNumber = 0;
            return _skinNumber;
        }

        private int DecreaseSkinNumber()
        {
            if (--_skinNumber < 0) _skinNumber = _skinCounts-1;
            return _skinNumber;
        }

        private void Play()
        {
            gameObject.SetActive(false);
            _player.CanBeControl = true;
            _player.SetCollider();
        }
    }
}