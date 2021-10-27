using Units;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class GameSettings : ScriptableObject
    {
        [Range(0,30)] public int asteroidsOnStart;
        [Tooltip(
            "Asteroids spawn rate in seconds. Can be more then 10 and less then 1 (but clamp in 0)," +
            " just write value you need in text field")]
        [Range(10, 1)] public float asteroidsSpawnRate;

        [Space]
        public int playerHp;
        public PlayerControlType playerControlType;
        public float playerSpeed;
        public float bulletSpeed;
    }
}