using System;
using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Units
{
    public class PlayerFabric
    {
        private readonly UnityAction _nextSkinAction;
        private readonly UnityAction _prevSkinAction;

        private Player _player;

        private float _bulletSpeed;
        private int _spriteNumber;
        private PlayerControlType _playerControlType;
            
        private readonly List<Sprite> _sprites = new List<Sprite>();

        public PlayerFabric(Player player)
        {
            _spriteNumber = 0;
            _nextSkinAction = NextSkinAction;
            _prevSkinAction = PrevSkinAction;
            _player = player;
            _playerControlType = 0;

            var sprites = Resources.LoadAll<Sprite>("Sprites/Ships");
            foreach (var sprite in sprites)
                _sprites?.Add(sprite);
            if (_sprites == null) throw new ArgumentNullException();
        }

        private void NextSkinAction()
        {
            if (++_spriteNumber > _sprites.Count - 1) _spriteNumber = 0;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
        }

        private void PrevSkinAction()
        {
            if (--_spriteNumber < 0) _spriteNumber = _sprites.Count-1;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
        }
        
        public void ChangeControlType()
        {
            if ((int)++_playerControlType > Enum.GetNames(typeof(PlayerControlType)).Length)
                _playerControlType = 0;
        }

        public void SetPlayer()
        {
            _player = Object.Instantiate(_player);
            _player.transform.position = Vector3.zero;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
            var bulletFabric = new BulletFabric(_player);
            _player.Fire += () => bulletFabric.Fire();
            _player.SetSettings();
            _bulletSpeed = GameSettings.Settings.BulletSpeed;
        }

        public Player GetPlayer() => _player;
    }
}