using UnityEngine;
using static Assets.SOAssets;
using Random = UnityEngine.Random;

namespace Scene
{
    public struct SceneBorders
    {
        public Vector2 TopBorder, RightBorder;

        public SceneBorders(Vector2 topBorder, Vector2 rightBorder)
        {
            TopBorder = topBorder;
            RightBorder = rightBorder;
        }

        public void InflateBorders(float arg)
        {
            TopBorder += new Vector2(0, arg);
            RightBorder += new Vector2(arg, 0);
        }
    }
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class SceneSetter : MonoBehaviour
    {
        [SerializeField] private Transform _backgroundTiles;
        
        private BoxCollider2D _collider;
        
        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            SetScene(_collider);
            SetBackground(SO.assets, _collider);
        }

        private void SetScene(BoxCollider2D collider)
        {
            Camera cum = Camera.allCameras[0];
            var sceneBorders = new SceneBorders(
                 cum.ScreenToWorldPoint(new Vector2(Screen.width/2f, Screen.height)),
                 cum.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height/2f))
            );
            sceneBorders.InflateBorders(1f); // Магическое число для раздувания границ
            collider.size = new Vector2(sceneBorders.RightBorder.x * 2, sceneBorders.TopBorder.y * 2);
        }

        private void SetBackground(Assets.Assets assets, BoxCollider2D collider)
        {
            Sprite bg = assets.backgrounds[Random.Range(0, assets.backgrounds.Length)];

            Vector2Int sceneBordersX = new Vector2Int(
                Mathf.FloorToInt(collider.bounds.min.x),
                Mathf.CeilToInt(collider.bounds.max.x)
                );
            Vector2Int sceneBordersY = new Vector2Int(
                Mathf.FloorToInt(collider.bounds.min.y),
                Mathf.CeilToInt(collider.bounds.max.y)
            );
            
            for (int x = sceneBordersX.x; x < sceneBordersX.y; x++)
            for (int y = sceneBordersY.x; y < sceneBordersY.y; y++)
            {
                var go = new GameObject("tile");
                var spriteRenderer = go.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = bg;
                go.transform.position = new Vector2(x, y);
                go.transform.SetParent(_backgroundTiles);
            }
        }
    }
}