using UnityEngine;

namespace Scripts.Car
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private const string vertical = "Vertical";
        private const string horizontal = "Horizontal";
        private const float _speed = 4f;
        private float positionX;

        private void FixedUpdate() =>
            Move();

        private void Move()
        {
            var positionMove = new Vector3(Input.GetAxis(horizontal), Input.GetAxis(vertical));
            _rigidbody2D.velocity = positionMove * _speed;
            BorderPosition();
        }

        private void BorderPosition()
        {
            Vector3 carPosition = transform.position;
            transform.position = new Vector3(Mathf.Clamp(carPosition.x, -1, 1), carPosition.y,
                carPosition.z);
        }
    }
}