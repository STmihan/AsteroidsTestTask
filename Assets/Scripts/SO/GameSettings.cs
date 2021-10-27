using Units;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class GameSettings : ScriptableObject
    {
        [Range(1,30)] public int asteroidsCount;
        [Range(1,10)] public float asteroidsSpeed;

        [Space]
        [Range(1,10)] public int playerHp;
        [Range(1,10)] public float playerSpeed;
        [Range(5,25)] public float bulletSpeed;
        public PlayerControlType playerControlType;
    }
}