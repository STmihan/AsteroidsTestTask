using System.Collections;
using Settings;
using UnityEngine;

namespace Units
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private VFX _vfx;
        private Player _player;
        private float _speed;
        public void Fire(in Player player)
        {
            _speed = GameSettings.Settings.BulletSpeed;
            StartCoroutine(MoveRoutine());
            Destroy(gameObject, 2f);
            _player = player;
        }

        private IEnumerator MoveRoutine()
        {
            while (true)
            {
                transform.Translate(Vector2.up * _speed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<Asteroid>()) return;
            Instantiate(_vfx, transform.position, transform.rotation);
            _player.ScoreUp?.Invoke();
        }
    }
}