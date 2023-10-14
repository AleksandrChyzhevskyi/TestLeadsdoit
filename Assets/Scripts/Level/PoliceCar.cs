using System;
using System.Collections;
using Scripts.Car;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    enum Side
    {
        Left = -1,
        Right = 1
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class PoliceCar : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _accelerationTime;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private Car _car;

        private void Start() => 
            StartPursuit(_car);

        private void StartPursuit(Car car)
        {
            _car = car;
            StartCoroutine(Pursuit(car));
        }

        private IEnumerator Pursuit(Car car)
        {
            Vector2 direction;
            Side side = Random.Range(0, 1) == 0 ? Side.Left : Side.Right;

            while (Mathf.Abs(car.transform.position.y - transform.position.y) >= 0.5f)
            {
                direction = transform.position - new Vector3(car.transform.position.x + (int)side,
                    car.transform.position.y, 0);
                
                Debug.Log(direction);
                
                _rigidbody2D.AddForce(direction * -1);

                yield return new WaitForFixedUpdate();
                ClampVelocity();
            }
        }
        
        private void ClampVelocity()
        {
            Vector2 velocity = _rigidbody2D.velocity;

            if (velocity.y > _maxSpeed)
                _rigidbody2D.velocity = new Vector2(velocity.x, _maxSpeed);
        }
    }
}