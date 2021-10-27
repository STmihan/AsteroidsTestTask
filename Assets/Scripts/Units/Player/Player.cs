using System;
using System.Collections;
using UnityEngine;
using static Assets.SOAssets;

namespace Units
{
    public enum PlayerControlType
    {
        Physics,
        NoPhysics
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _firePoint;
        
        public bool CanBeControl { get; set; }

        public Action ScoreUp;
        public Action OnDeath;

        public int Score { get; private set; }
        public PlayerControlType ControlType { get; private set; }

        private int _hp;
        private float _speed;
        private bool _isTakenHitRecently;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        public void SetPlayerSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            var settings = SO.settings;
            _hp = settings.playerHp;
            ControlType = settings.playerControlType;
            _speed = settings.playerSpeed;

            OnDeath += () =>
            {
                CanBeControl = false;
                Destroy(gameObject);
            };
        }

        public void TakeHit()
        {
            if(_isTakenHitRecently) return;
            if(--_hp<=0) OnDeath?.Invoke();
            else StartCoroutine(TakeHitGfx());
        }

        private IEnumerator TakeHitGfx()
        {
            var col = GetComponent<PolygonCollider2D>();
            col.isTrigger = true;
            _isTakenHitRecently = true;
            _spriteRenderer.color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = new Color(1,1,1,1);
            _isTakenHitRecently = false;
            col.isTrigger = false;

        }
        
        // Долго думал над тем какое управление будет удобно,
        // чтобы всегда W вело корабль вверх, а остальные клавиши
        // в свои стороны, или же чтобы w вело корабль в сторону, в которую он смотрит,
        // ну и остальные кнопки тоже передвигали корабль относительно его локальных координат.
        // Я в итоге выбрал второй метод, но если что, то в комментариях оставляю вариант с
        // передвижением, относительно глобальной системы координат.
        public void Move(Vector2 input)
        {
            if(!CanBeControl) return;
            transform.Translate(input * _speed * Time.fixedDeltaTime);
            //_rigidbody.MovePosition(_rigidbody.position + input * _speed * Time.fixedDeltaTime); 
        }

        public void MovePhysics(Vector2 input)
        {
            if(!CanBeControl) return;
            _rigidbody.AddRelativeForce(input * _speed);
            // _rigidbody.AddForce(input * _speed);
        }

        public void Rotate(Vector2 input)
        {
            if(!CanBeControl) return;
            Vector2 direction = input - _rigidbody.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.SetRotation(angle);
        }

        public void Shoot()
        {
            if(!CanBeControl) return;
            var bullet = Instantiate(_bullet);
            bullet.transform.position = _firePoint.position;
            Vector2 direction = (Vector2)_firePoint.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            bullet.transform.rotation = Quaternion.Euler(0,0,angle);
            bullet.Fire();
        }

        public void SetCollider()
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}