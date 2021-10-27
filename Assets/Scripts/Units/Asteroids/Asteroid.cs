using System.Collections;
using Scene;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : MonoBehaviour
    {
        public SceneBorders Borders { private get; set; }
        
        public Player Player { private get; set; }
        
        public float Speed { private get; set; }

        private IEnumerator Start()
        {
            Vector2 direction = new Vector2(Random.Range(-1, 1), Random.Range(-1,1)) - (Vector2)transform.position;
            while (true)
            {
                transform.Translate(direction.normalized * Speed * Time.deltaTime);
                yield return null;
            }
        }

        public void SetSprite(Sprite sprite)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 1;
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
                Player.ScoreUp?.Invoke();
                Destroy(gameObject);
            }
            if (other.TryGetComponent(out Player player)) player.TakeHit();
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