using System;
using Settings;
using UnityEngine;

namespace Assets
{
    public class SOAssets : MonoBehaviour
    {
        public static SOAssets SO;

        public GameSettings settings;
        public Assets assets;

        private void Awake() => SO = this;
    }
}