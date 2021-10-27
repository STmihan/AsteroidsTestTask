﻿using System;
using TMPro;
using Units;
using UnityEngine;

namespace UI
{
    public class ScoreUI : MonoBehaviour, IScreen
    {

        private int _score;

        [SerializeField] private TextMeshProUGUI _text;
        
        private Player _player;
        public void SetPlayer(Player player) => _player = player;
        
        public void Init()
        {
            _player.ScoreUp += () => _text.SetText(_score.ToString());
        }
    }
}