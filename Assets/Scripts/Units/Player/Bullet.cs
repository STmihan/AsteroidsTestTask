using System.Collections;
using UnityEngine;

namespace Units
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        public void Fire(float bulletSpeed)
        {
            _speed = bulletSpeed;
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
    }
}