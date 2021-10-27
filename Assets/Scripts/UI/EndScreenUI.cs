using System.Collections;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndScreenUI : MonoBehaviour, IScreen
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _waitBeforeShow;

        private Canvas _canvas;
        
        private Player _player;
        public void SetPlayer(Player player) => _player = player;

        public void Init()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            _player.ScoreUp += () => _text.SetText(
                $"Game Over!<br><br>{_player.Score}<br><br>Press Space<br>To restart");
            _player.OnDeath += () => StartCoroutine(ShowScore());
        }

        private IEnumerator ShowScore()
        {
            yield return new WaitForSeconds(_waitBeforeShow);
            _canvas.enabled = true;
            while (!Input.GetKeyDown(KeyCode.Space)) yield return null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}