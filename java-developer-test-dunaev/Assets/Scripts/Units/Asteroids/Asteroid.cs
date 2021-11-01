using System.Collections;
using Scene;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : MonoBehaviour
    {
        public float Speed { private get; set; }

        public void SetSprite(Sprite sprite)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 1;
        }

        public void SetCollider() => gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;

        private IEnumerator Start()
        {
            Vector2 direction = new Vector2(Random.Range(-1, 1), Random.Range(-1,1)) - (Vector2)transform.position;
            while (true)
            {
                transform.Translate(direction.normalized * Speed * Time.deltaTime);
                yield return null;
            }
        }

        private void Update()
        {
            ScreenWrap();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Bullet>())
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            if (other.TryGetComponent(out Player player)) player.TakeHit();
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