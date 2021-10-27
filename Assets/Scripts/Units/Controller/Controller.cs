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
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0)) _player.Shoot();
        }
        
        private void Move()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            switch (_player.ControlType)
            {
                case PlayerControlType.Physics:
                    _player.MovePhysics(input);
                    break;
                case PlayerControlType.NoPhysics:
                    _player.Move(input);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Rotate() => _player.Rotate(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}