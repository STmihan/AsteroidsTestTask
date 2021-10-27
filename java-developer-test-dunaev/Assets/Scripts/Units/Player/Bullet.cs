using System.Collections;
using UnityEngine;

namespace Units
{
    public class Bullet : MonoBehaviour
    {
        private VFX _vfx;
        private float _speed;
        public void Fire(float bulletSpeed, VFX vfx)
        {
            _speed = bulletSpeed;
            _vfx = vfx;
            StartCoroutine(MoveRoutine());
            Destroy(gameObject, 2f);
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
            if (other.gameObject.GetComponent<Asteroid>()) Instantiate(_vfx, transform.position, transform.rotation);
        }
    }
}