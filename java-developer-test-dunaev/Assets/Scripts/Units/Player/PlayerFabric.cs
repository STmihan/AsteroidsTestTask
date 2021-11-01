using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Units
{
    public class PlayerFabric
    {
        private Player _player;

        private int _spriteNumber;
            
        private readonly List<Sprite> _sprites = new List<Sprite>();

        public PlayerFabric(Player player)
        {
            _spriteNumber = 0;
            _player = player;

            var sprites = Resources.LoadAll<Sprite>("Ships");
            foreach (var sprite in sprites)
                _sprites?.Add(sprite);
            if (_sprites == null) throw new ArgumentNullException();
        }

        public void NextSkin()
        {
            if (++_spriteNumber > _sprites.Count - 1) _spriteNumber = 0;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
        }

        public void PrevSkin()
        {
            if (--_spriteNumber < 0) _spriteNumber = _sprites.Count-1;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
        }
        
        public void SetPlayer()
        {
            _player = Object.Instantiate(_player);
            _player.transform.position = Vector3.zero;
            _player.SetPlayerSprite(_sprites[_spriteNumber]);
            var bulletFabric = new BulletFabric(_player);
            _player.Fire += () => bulletFabric.Fire();
            _player.SetSettings();
        }

        public Player GetPlayer() => _player;
    }
}