using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "Game Assets", menuName = "Game Assets")]
    public class Assets : ScriptableObject
    {
        public Sprite[] backgrounds;
        public Sprite[] playerShips;
        public Sprite[] bigAsteroids;
        public Sprite[] mediumAsteroids;
        public Sprite[] smallAsteroids;
        public Sprite[] tinyAsteroids;
    }
}