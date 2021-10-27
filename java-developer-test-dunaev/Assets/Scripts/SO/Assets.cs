using Units;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Game Assets", menuName = "Game Assets")]
    public class Assets : ScriptableObject
    {
        public Sprite[] backgrounds;
        public Sprite[] playerShips;
        public Sprite[] asteroids;
        [Header("Prefab")] 
        public Bullet bullet;
        public Player player;
        [Header("VFX")] 
        public VFX bulletHitVFX;
        public VFX playerDestroyVFX;
    }
}