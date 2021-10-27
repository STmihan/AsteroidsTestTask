using System;
using UnityEngine;
using static Assets.SOAssets;

namespace Units
{
    public enum PlayerControlType
    {
        Physics,
        NoPhysics
    }
    
    public class Player : MonoBehaviour
    {
        public Action OnDeath;

        private int _hp;
        private PlayerControlType _controlType;
        
        private SpriteRenderer _spriteRenderer;
        public void SetPlayerSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var settings = SO.settings;
            _hp = settings.playerHp;
            _controlType = settings.playerControlType;
        }

        public void TakeHit()
        {
            if(--_hp<=0) OnDeath?.Invoke();
        }

        public void Move(Vector2 input)
        {
            
        }

        public void MovePhysics(Vector2 input)
        {
            
        }

        public void Rotate(Vector2 input)
        {
            
        }
    }
}