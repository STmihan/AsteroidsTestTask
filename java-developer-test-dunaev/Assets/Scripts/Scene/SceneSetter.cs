using Settings;
using UI;
using Units;
using UnityEngine;

namespace Scene
{
    public class SceneSetter : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private void Awake()
        {
            var backgroundSetter = new BackgroundSetter();
            backgroundSetter.SetBackground();

            PlayerFabric playerFabric = new PlayerFabric(_player);
            playerFabric.SetPlayer();
            _player = playerFabric.GetPlayer();

            var asteroidSetter = new AsteroidFabric(_player);

            var ui = new UI.UI();
            ui.SetBindings(Play, playerFabric);

            void Play()
            {
                for (int i = 0; i < GameSettings.Settings.AsteroidsCount; i++)
                {
                    asteroidSetter.SpawnAsteroid(new Vector2(3,3));
                    _player.CanBeControl = true;
                    FindObjectOfType<SkinSelectorUI>().Canvas.enabled = false;
                    _player.SetCollider();
                }
            }
        }
    }
}