using System;
using System.Collections;
using SO;
using UI;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scene
{
    public class SceneSetter : MonoBehaviour
    {
        
        [SerializeField] private Transform _backgroundTilesParent; 
        private Player _player;

        [Space]
        
        [SerializeField] private SkinSelectorUI _skinSelectorUI;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private EndScreenUI _endScreenUI;

        [Space] 
        [SerializeField] private Assets _assets;
        [SerializeField] private GameSettings _settings;
        
        private SceneBorders _borders;
        
        private void Start()
        {
            _player = _assets.player;
            SetScene();
            SetBackground();
            SetPlayer();
            SetUI();
            StartCoroutine(SetAsteroids());
            _player.ScoreUp += () =>
            {
                var b = new SceneBorders();
                Vector2 v = new Vector2(b.Border.x, b.Border.y);
                SpawnAsteroid(v);
            };
        }

        private void SetScene()
        {
            Camera cum = Camera.allCameras[0];
            _borders = new SceneBorders();
        }

        private void SetBackground()
        {
            Sprite bg = _assets.backgrounds[Random.Range(0, _assets.backgrounds.Length)];

            Vector2Int sceneBordersX = new Vector2Int(
                Mathf.FloorToInt(-_borders.InflatedBorders().x),
                Mathf.CeilToInt(_borders.InflatedBorders().x)
                );
            Vector2Int sceneBordersY = new Vector2Int(
                Mathf.FloorToInt(-_borders.InflatedBorders().y),
                Mathf.CeilToInt(_borders.InflatedBorders().y)
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
            _player.Borders = _borders;
            _player.SetBullet(_assets.bullet);
            _player.SetVFX(_assets.bulletHitVFX, _assets.playerDestroyVFX);
            _player.SetSettings(_settings);
        }

        private void SetUI()
        {
            _skinSelectorUI.SetSprites(ref _assets.playerShips);
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
            for (int i = 0; i < _settings.asteroidsCount; i++)
            {
                SpawnAsteroid(new Vector2(3,2));
            }
        }

        private Asteroid SpawnAsteroid(Vector2 boxBounds)
        {
            var go = new GameObject("Asteroid");
            var asteroid = go.AddComponent<Asteroid>();
            var bounds = _borders.Border;
            asteroid.transform.position = RandomSpawn();
            asteroid.Borders = _borders;
            asteroid.Speed = _settings.asteroidsSpeed;
            asteroid.SetSprite(_assets.asteroids[Random.Range(0,_assets.asteroids.Length)]);
            asteroid.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
            asteroid.Player = _player;


            Vector2 RandomSpawn()
            {
                var finalBound = new Vector2(
                    Random.Range(boxBounds.x, bounds.x),
                    Random.Range(boxBounds.y, bounds.y));
                return Random.Range(0,4) switch
                {
                    0 => finalBound,
                    1 => new Vector2(-finalBound.x, finalBound.y),
                    2 => new Vector2(finalBound.x, -finalBound.y),
                    3 => new Vector2(-finalBound.x, -finalBound.y),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return asteroid;
        }
    }
}