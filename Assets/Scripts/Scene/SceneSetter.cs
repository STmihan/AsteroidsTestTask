using System.Collections;
using UI;
using Units;
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
        
        [SerializeField] private Transform _backgroundTilesParent;
        [SerializeField] private Player _player;

        [Space]
        
        [SerializeField] private SkinSelectorUI _skinSelectorUI;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private EndScreenUI _endScreenUI;

        private BoxCollider2D _sceneBorders;
        
        private void Start()
        {
            _sceneBorders = GetComponent<BoxCollider2D>();
            SetScene();
            SetBackground();
            SetPlayer();
            SetUI();
            StartCoroutine(SetAsteroids());
        }

        private void SetScene()
        {
            Camera cum = Camera.allCameras[0];
            var sceneBorders = new SceneBorders(
                 cum.ScreenToWorldPoint(new Vector2(Screen.width/2f, Screen.height)),
                 cum.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height/2f))
            );
            sceneBorders.InflateBorders(1f); // Магическое число для раздувания границ
            _sceneBorders.size = new Vector2(sceneBorders.RightBorder.x * 2, sceneBorders.TopBorder.y * 2);
        }

        private void SetBackground()
        {
            Sprite bg = SO.assets.backgrounds[Random.Range(0, SO.assets.backgrounds.Length)];

            Vector2Int sceneBordersX = new Vector2Int(
                Mathf.FloorToInt(_sceneBorders.bounds.min.x),
                Mathf.CeilToInt(_sceneBorders.bounds.max.x)
                );
            Vector2Int sceneBordersY = new Vector2Int(
                Mathf.FloorToInt(_sceneBorders.bounds.min.y),
                Mathf.CeilToInt(_sceneBorders.bounds.max.y)
            );
            
            for (int x = sceneBordersX.x; x < sceneBordersX.y; x++)
            for (int y = sceneBordersY.x; y < sceneBordersY.y; y++)
            {
                var go = new GameObject("tile");
                var spriteRenderer = go.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = bg;
                go.transform.position = new Vector2(x, y);
                go.transform.SetParent(_backgroundTilesParent);
            }
        }

        private void SetPlayer()
        {
            _player = Instantiate(_player);
            _player.transform.position = Vector2.zero;
        }

        private void SetUI()
        {
            _skinSelectorUI.SetPlayer(_player);
            _skinSelectorUI.Init();
            
            _scoreUI.SetPlayer(_player);
            _scoreUI.Init();
            
            _endScreenUI.SetPlayer(_player);
            _endScreenUI.Init();
        }
        
        private IEnumerator SetAsteroids()
        {
            while (!_player.CanBeControl) yield return null;
            for (int i = 0; i < SO.settings.asteroidsOnStart; i++)
            {
                var go = new GameObject("Asteroid");
                var asteroid = go.AddComponent<Asteroid>();
                asteroid.Type = (AsteroidType) Random.Range(0, 2);
                var bounds = _sceneBorders.bounds;
                var pos = RandomSpawn();
                if (pos.x > -3f && pos.x < 3f) RandomSpawn();
                if (pos.y > -2f && pos.y < 2f) RandomSpawn();
                asteroid.transform.position = pos;

                Vector2 RandomSpawn() => new Vector2(
                        Random.Range(bounds.min.x, bounds.max.x),
                        Random.Range(bounds.min.y, bounds.max.y));
            }
        }
    }
}