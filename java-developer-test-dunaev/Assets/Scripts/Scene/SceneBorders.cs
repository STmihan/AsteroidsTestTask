using UnityEngine;

namespace Scene
{
    public static class SceneBorders
    {
        public static Vector2 Border;

        static SceneBorders()
        {
            Border.x = Camera.allCameras[0].ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            Border.y = Camera.allCameras[0].ScreenToWorldPoint(new Vector2(0, Screen.height)).y; 
        }
    }

}