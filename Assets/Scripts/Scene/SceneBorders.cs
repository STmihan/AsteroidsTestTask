using UnityEngine;

namespace Scene
{
    public class SceneBorders
    {
        public Vector2 Border;

        public SceneBorders()
        {
            Border.x = Camera.allCameras[0].ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            Border.y = Camera.allCameras[0].ScreenToWorldPoint(new Vector2(0, Screen.height)).y; 
        }

        public Vector2 InflatedBorders() => new Vector2(Border.x + 1, Border.y + 1);
    }

}