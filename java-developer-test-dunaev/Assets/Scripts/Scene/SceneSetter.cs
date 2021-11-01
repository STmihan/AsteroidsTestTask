using Settings;
using Units;
using UnityEngine;

namespace Scene
{
    public class SceneSetter : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private void Start()
        {
            var backgroundSetter = new BackgroundSetter();
            backgroundSetter.SetBackground();

            PlayerFabric playerFabric = new PlayerFabric(_player);
            playerFabric.SetPlayer();
            _player = playerFabric.GetPlayer();

            var asteroidSetter = new AsteroidFabric(_player);
            
            PlayCallback();

            // var ui = new UI.UI(_player) { PlayCallback = PlayCallback };

            void PlayCallback()
            {
                for (int i = 0; i < GameSettings.Settings.AsteroidsCount; i++) asteroidSetter.SpawnAsteroid(new Vector2(3, 2));
                _player.CanBeControl = true;
                _player.SetCollider();
            }
        }
    }
}