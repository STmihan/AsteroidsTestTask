using UnityEngine;
using static UnityEngine.Object;

namespace Units
{
    public class BulletFabric
    {
        private readonly Player _player;
        private readonly Bullet _bullet;

        public BulletFabric(Player player)
        {
            _player = player;
            _bullet = _player.GetBulletPrefab();
        }

        public void Fire()
        {
            var bullet = Instantiate(_bullet);
            bullet.transform.position = _player.GetFirePointPosition();
            Vector2 direction = (Vector2)_player.GetFirePointPosition() - (Vector2)_player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            bullet.transform.rotation = Quaternion.Euler(0,0,angle);
            bullet.Fire(_player);
        }
    }
}