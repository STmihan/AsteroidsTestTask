using System;
using System.Collections;
using UnityEngine;
using static Assets.SOAssets;

namespace Units
{
    public class Bullet : MonoBehaviour
    {
        private float _bulletSpeed;
        public void Fire()
        {
            _bulletSpeed = SO.settings.bulletSpeed;
            StartCoroutine(MoveRoutine());
            Destroy(gameObject, 2f);
        }

        private IEnumerator MoveRoutine()
        {
            while (true)
            {
                transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}