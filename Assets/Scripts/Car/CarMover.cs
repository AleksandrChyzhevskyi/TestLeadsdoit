using UnityEngine;

namespace Scripts.Car
{
    public class CarMover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private InputManager _inputManager;
        private CarStats _carStats;

        private void FixedUpdate() =>
            Move();

        public void Instantiate(InputManager inputManager, Rigidbody2D rigidbody2D, CarStats carStats)
        {
            _rigidbody2D = rigidbody2D;
            _inputManager = inputManager;
            _carStats = carStats;
        }

        private void Move()
        {
            if (_inputManager.Rotate != 0)
            {
                _carStats.RotateDirection = new Vector2(_inputManager.Rotate, 0);
                _rigidbody2D.AddForce(_carStats.RotateDirection.normalized, ForceMode2D.Impulse);
            }

            if (_inputManager.OnGas)
                OnGas();

            if (_inputManager.OnBrake)
                OnBack();

            ClampVelocity();
        }

        private void OnGas() =>
            _rigidbody2D.AddForce(Vector2.up * CarStats.GasMultiplayer, ForceMode2D.Force);

        private void OnBack()
        {
            var direction = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector2(direction.x,
                Mathf.MoveTowards(direction.y, 0, CarStats.GasMultiplayer * Time.deltaTime));
        }

        private void ClampPosition()
        {
            Vector3 carPosition = transform.position;
            transform.position = new Vector3(Mathf.Clamp(carPosition.x, -1, 1), carPosition.y,
                0);
        }

        private void ClampVelocity()
        {
            Vector2 velocity = _rigidbody2D.velocity;

            if (velocity.y > _carStats.MaxSpeed)
                _rigidbody2D.velocity = new Vector2(velocity.x, _carStats.MaxSpeed);

            if ((_rigidbody2D.position.x > 1 && velocity.x > 0) || (_rigidbody2D.position.x < -1 && velocity.x < 0))
                _rigidbody2D.velocity = new Vector2(0, velocity.y);
        }
    }
}