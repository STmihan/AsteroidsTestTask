using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        public Action ScoreUp;

        private int _score;

        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            ScoreUp += () =>
            {
                _score++;
                _text.SetText(_score.ToString());
            };
        }
    }
}