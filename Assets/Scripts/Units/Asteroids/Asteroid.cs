using System;
using UnityEngine;
using static Assets.SOAssets;
using Random = UnityEngine.Random;

namespace Units
{
    public enum AsteroidType
    {
        Big = 0,
        Medium = 1,
        Small = 2,
        Tiny = 3
    }
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : MonoBehaviour
    {
        public AsteroidType Type { get; set; }
        private Action _onShouldDestroy;

        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Type switch
            {
                AsteroidType.Big => SO.assets.bigAsteroids[Random.Range(0, SO.assets.bigAsteroids.Length)],
                AsteroidType.Medium => SO.assets.mediumAsteroids[Random.Range(0, SO.assets.mediumAsteroids.Length)],
                AsteroidType.Small => SO.assets.smallAsteroids[Random.Range(0, SO.assets.smallAsteroids.Length)],
                AsteroidType.Tiny => SO.assets.tinyAsteroids[Random.Range(0, SO.assets.tinyAsteroids.Length)],
                _ => throw new ArgumentOutOfRangeException()
            };
            spriteRenderer.sortingOrder = 1;
            gameObject.AddComponent<PolygonCollider2D>();

            _onShouldDestroy += () =>
            {
                switch (Type)
                {
                    case AsteroidType.Big:
                    case AsteroidType.Medium:
                    case AsteroidType.Small:
                        SpawnChildrenAsteroid(Type);
                        break;
                    case AsteroidType.Tiny:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                FindObjectOfType<Player>().ScoreUp?.Invoke();
                Destroy(gameObject);
            };
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player)) player.TakeHit();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Bullet>())
            {
                Destroy(other.gameObject);
                _onShouldDestroy?.Invoke();
            }
        }

        private void SpawnChildrenAsteroid(AsteroidType type)
        {
            var go = new GameObject("Asteroid");
            go.transform.position = transform.position;
            
            var asteroid = go.AddComponent<Asteroid>();
            asteroid.Type = type switch
            {
                AsteroidType.Big => AsteroidType.Medium,
                AsteroidType.Medium => AsteroidType.Small,
                AsteroidType.Small => AsteroidType.Tiny,
                AsteroidType.Tiny => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}