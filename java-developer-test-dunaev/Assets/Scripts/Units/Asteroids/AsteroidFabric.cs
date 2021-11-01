using System;
using System.Collections.Generic;
using Scene;
using Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
    public class AsteroidFabric
    {
        private readonly List<Sprite> _sprites = new List<Sprite>();

        public AsteroidFabric(Player player)
        {
            var sprites = Resources.LoadAll<Sprite>("Sprites/Asteroids");
            
            foreach (var sprite in sprites)
                _sprites?.Add(sprite);
            if (_sprites == null) throw new ArgumentNullException();

            player.ScoreUp += () => SpawnAsteroid(new Vector2(SceneBorders.Border.x, SceneBorders.Border.y));
        }

        public Asteroid SpawnAsteroid(Vector2 spawnOffsetFromCenter)
        {
            var go = new GameObject("Asteroid");
            var asteroid = go.AddComponent<Asteroid>();
            asteroid.transform.position = CalcSpawn(spawnOffsetFromCenter,
                new Vector2(SceneBorders.Border.x + 1, SceneBorders.Border.y + 1));
            asteroid.Speed = GameSettings.Settings.AsteroidsSpeed;
            asteroid.SetSprite(_sprites[Random.Range(0,_sprites.Count)]);
            asteroid.SetCollider();


            Vector2 CalcSpawn(Vector2 minRange, Vector2 maxRange)
            {
                var finalBound = new Vector2(
                    Random.Range(minRange.x, maxRange.x),
                    Random.Range(minRange.y, maxRange.y));
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