using System.IO;
using UnityEngine;


namespace Settings
{
    public class SettingsSetter : MonoBehaviour
    {
        private void Awake()
        {
            var settings = new GameSettings
            (
                4,
                3,
                2,
                2,
                10,
                1
            );
            var json = JsonUtility.ToJson(settings, true);
            using (StreamWriter writer = new StreamWriter(Application.dataPath + "/save.json"))
                writer.Write(json);
        }
    }
}