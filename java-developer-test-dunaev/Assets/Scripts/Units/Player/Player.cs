using System;
using System.Collections;
using Scene;
using Settings;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private VFX _playerDeathVFX;
        
        public Action ScoreUp;
        public Action OnDeath;
        public Action Fire;
        
        public bool CanBeControl { get; set; }

        public int Score { get; private set; }
        
        private int _hp;
        private float _speed;
        private bool _isTakenHitRecently;
        private float _invincibility;


        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        public void SetPlayerSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
        public void SetCollider() => gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        public void SetSettings()
        {
            _hp = GameSettings.Settings.PlayerHp;
            _speed = GameSettings.Settings.PlayerSpeed;
            _invincibility = GameSettings.Settings.PlayerInvincibilityTime;
        }

        public Bullet GetBulletPrefab() => _bullet;
        public Vector3 GetFirePointPosition() => _firePoint.transform.position;

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

        private void Update() => ScreenWrap();

        public void TakeHit()
        {
            if(_isTakenHitRecently) return;
            if(--_hp<=0) OnDeath?.Invoke();
            else StartCoroutine(StartInvincibility());
        }

        private IEnumerator StartInvincibility()
        {
            _isTakenHitRecently = true;
            for (int i = 0; i < 2; i++)
            {
                _spriteRenderer.color = new Color(1,1,1,0.5f);
                yield return new WaitForSeconds(_invincibility/4);
                _spriteRenderer.color = new Color(1,1,1,1);
                yield return new WaitForSeconds(_invincibility/4);
            }
            _isTakenHitRecently = false;
        }

        public void Move(Vector2 input)
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
            Fire?.Invoke();
        }
        
        private void ScreenWrap()
        {
            Vector2 pos = transform.position;
            if (transform.position.y > SceneBorders.Border.y) pos.y = -SceneBorders.Border.y;
            if (transform.position.y < -SceneBorders.Border.y) pos.y = SceneBorders.Border.y;
            if (transform.position.x > SceneBorders.Border.x) pos.x = -SceneBorders.Border.x;
            if (transform.position.x < -SceneBorders.Border.x) pos.x = SceneBorders.Border.x;
            transform.position = pos;
        }
    }
}