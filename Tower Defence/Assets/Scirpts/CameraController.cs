using UnityEngine;

namespace Scirpts
{
    public class CameraController : MonoBehaviour
    {

        private float _panSpeed = 0.1f;
        private float _pixelBuffer = 10;

        private bool _doMovement = true;
        private float _scrollSpeed = 5;
        private float _minY = 10;
        private float _maxY = 300;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _doMovement = !_doMovement;
            }
            if (!_doMovement)
            {
                return;
            }
            HandleMovement();
            HandleRotation();
        }

        private void HandleRotation()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 pos = transform.position;

            pos.y -= scroll * _scrollSpeed * Time.deltaTime * 100;
            pos.y = Mathf.Clamp(pos.y, _minY, _maxY);

            transform.position = pos;
        }

        private void HandleMovement()
        {
            Vector3 mov = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - _pixelBuffer)
            {
                mov.z = 1;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= _pixelBuffer)
            {
                mov.x = -1;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= _pixelBuffer)
            {
                mov.z = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - _pixelBuffer)
            {
                mov.x = 1;
            }

            if (transform.position.x > 27.5 && mov.x > 0)
            {
                mov.x = 0;
            }
            if (transform.position.x < -32.5 && mov.x < 0)
            {
                mov.x = 0;
            }
            if (transform.position.z > 62.5 && mov.z > 0)
            {
                mov.z = 0;
            }
            if (transform.position.z < -2.5 && mov.z < 0)
            {
                mov.z = 0;
            }
            
            
            transform.Translate(mov.normalized * _panSpeed, Space.World);
        }
    }
}