using Unity.VisualScripting;
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
            ClampPosition();
        }

        private void ClampPosition()
        {
            Vector3 carPosition = transform.position;
            transform.position = new Vector3(Mathf.Clamp(carPosition.x, -1, 1), carPosition.y,
                carPosition.z);
        }

        private void OnGas() =>
            _rigidbody2D.AddForce(Vector2.up, ForceMode2D.Force);

        private void OnBack()
        {
            var rigidbody2DVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector2(rigidbody2DVelocity.x,
                Mathf.MoveTowards(rigidbody2DVelocity.y, 0, _speed * Time.deltaTime));
        }
    }
}