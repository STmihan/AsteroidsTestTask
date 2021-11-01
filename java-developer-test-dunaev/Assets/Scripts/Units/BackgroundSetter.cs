using Scene;
using UnityEngine;

namespace Units
{
    public class BackgroundSetter
    {
        private readonly Sprite _sprite;
        private readonly Transform _transform;

        public BackgroundSetter()
        {
            var sprites = Resources.LoadAll<Sprite>("Sprites/Backgrounds");
            _sprite = sprites[Random.Range(0, sprites.Length)];
            _transform = new GameObject("Background").transform;
        }
        
        public void SetBackground()
        {
            Vector2 borders = new Vector2(SceneBorders.Border.x + 1, SceneBorders.Border.y + 1);
            Vector2Int sceneBordersX = new Vector2Int(
                Mathf.FloorToInt(-borders.x),
                Mathf.CeilToInt(borders.x)
            );
            Vector2Int sceneBordersY = new Vector2Int(
                Mathf.FloorToInt(-borders.y),
                Mathf.CeilToInt(borders.y)
            );
            
            for (int x = sceneBordersX.x; x < sceneBordersX.y; x++)
            for (int y = sceneBordersY.x; y < sceneBordersY.y; y++)
            {
                var go = new GameObject("tile");
                var spriteRenderer = go.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = _sprite;
                go.transform.position = new Vector2(x, y);
                go.transform.SetParent(_transform);
            }
        }
    }
}