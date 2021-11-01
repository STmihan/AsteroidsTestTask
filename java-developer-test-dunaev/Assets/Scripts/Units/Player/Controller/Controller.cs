using System;
using UnityEngine;

namespace Units
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private Camera _camera;

        private void Start() => _camera = Camera.allCameras[0];

        private void Update()
        {
            Shoot();
            if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
            Rotate();
        }

        private void FixedUpdate() => Move();

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0)) _player.Shoot();
        }
        
        private void Move() => _player.Move(new Vector2(Input.GetAxis("Horizontal"), Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1)));

        private void Rotate() => _player.Rotate(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}