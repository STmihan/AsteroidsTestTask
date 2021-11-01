using System;
using System.IO;
using UnityEngine;

namespace Settings
{
    
    [Serializable]
    public class GameSettings
    {
        public static GameSettings Settings;
        
        static GameSettings()
        {
            var json = "";
            using (StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/Settings.json"))
            {
                string line;
                while ((line = reader.ReadLine()) != null) json += line;
            }
        
            if (string.IsNullOrEmpty(json))
            {
                Settings = new GameSettings();
                return;
            }
            Settings = JsonUtility.FromJson<GameSettings>(json);
        }
        
        public GameSettings()
        {
            AsteroidsCount = 4;
            AsteroidsSpeed = 3;
            PlayerHp = 2;
            PlayerSpeed = 2;
            BulletSpeed = 10;
            PlayerInvincibilityTime = 1;
        }

        public GameSettings(int asteroidsCount, float asteroidsSpeed, int playerHp, float playerSpeed, float bulletSpeed, float playerInvincibilityTime)
        {
            AsteroidsCount = asteroidsCount;
            AsteroidsSpeed = asteroidsSpeed;
            PlayerHp = playerHp;
            PlayerSpeed = playerSpeed;
            BulletSpeed = bulletSpeed;
            PlayerInvincibilityTime = playerInvincibilityTime;
        }

        public int AsteroidsCount;
        public float AsteroidsSpeed;
        public int PlayerHp;
        public float PlayerSpeed;
        public float BulletSpeed;
        public float PlayerInvincibilityTime;
    }
}