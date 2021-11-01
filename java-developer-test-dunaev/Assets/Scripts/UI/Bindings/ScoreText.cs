using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreText : MonoBehaviour
    {
        public Action<int> Change;

        private void Awake()
        {
            var text = GetComponent<TextMeshProUGUI>();
            Change += i => text.text = i.ToString();
        }
    }
}