using System;
using System.Collections;
using Scene;
using SO;
using UnityEngine;

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
        [SerializeField] private Transform _firePoint;
        
        public bool CanBeControl { get; set; }

        public Action ScoreUp;
        public Action OnDeath;

        public int Score { get; private set; }
        public PlayerControlType ControlType { get; private set; }

        private int _hp;
        private float _speed;
        private float _bulletSpeed;
        private bool _isTakenHitRecently;
        
        private Bullet _bullet;
        private VFX _bulletVFX;
        private VFX _playerDeathVFX;

        public SceneBorders Borders { private get; set; }

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        public void SetPlayerSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
        public void SetCollider() => gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        public void SetBullet(Bullet bullet)
        {
            _bullet = bullet;
        }

        public void SetVFX(VFX bulletVfx, VFX playerDeathVfx)
        {
            _bulletVFX = bulletVfx;
            _playerDeathVFX = playerDeathVfx;
        }

        private void Awake()
        {
            Score = 0;
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            OnDeath += () =>
            {
                CanBeControl = false;
                Instantiate(_playerDeathVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            };

            ScoreUp += () => ++Score;
        }

        public void SetSettings(GameSettings settings)
        {
            _hp = settings.playerHp;
            ControlType = settings.playerControlType;
            _speed = settings.playerSpeed;
            _bulletSpeed = settings.bulletSpeed;
        }

        private void Update()
        {
            ScreenWrap();
        }

        public void TakeHit()
        {
            if(_isTakenHitRecently) return;
            if(--_hp<=0) OnDeath?.Invoke();
            else StartCoroutine(TakeHitGfx());
        }

        private IEnumerator TakeHitGfx()
        {
            _isTakenHitRecently = true;
            for (int i = 0; i < 2; i++)
            {
                _spriteRenderer.color = new Color(1,1,1,0.5f);
                yield return new WaitForSeconds(0.3f);
                _spriteRenderer.color = new Color(1,1,1,1);
                yield return new WaitForSeconds(0.2f);
            }
            _isTakenHitRecently = false;
        }
        
        public void Move(Vector2 input)
        {
            if(!CanBeControl) return;
            transform.Translate(input * _speed * Time.fixedDeltaTime);
        }

        public void MovePhysics(Vector2 input)
        {
            if(!CanBeControl) return;
            _rigidbody.AddRelativeForce(input * _speed);
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
            bullet.Fire(_bulletSpeed, _bulletVFX);
        }
        
        private void ScreenWrap()
        {
            Vector2 pos = transform.position;
            if (transform.position.y > Borders.Border.y) pos.y = -Borders.Border.y;
            if (transform.position.y < -Borders.Border.y) pos.y = Borders.Border.y;
            if (transform.position.x > Borders.Border.x) pos.x = -Borders.Border.x;
            if (transform.position.x < -Borders.Border.x) pos.x = Borders.Border.x;
            transform.position = pos;
        }

    }
}