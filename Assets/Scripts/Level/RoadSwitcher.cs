using System.Collections;
using System.Collections.Generic;
using Builder;
using UnityEngine;

namespace Level
{
    public class RoadSwitcher : MonoBehaviour
    {
        [SerializeField] private Road _currentRoad;
        [SerializeField] private Camera _camera;
        [SerializeField] private RoadBuilder _roadBuilder;
        [SerializeField] private PoolObstacles _poolObstacles;
        [SerializeField] private PoolBoosters _poolBoosters;

        private readonly Queue<Road> _queueRoads = new Queue<Road>();
        private Road _nextRoad;
        private Road _previousRoad;
        private Vector2 _size;

        private void Awake() =>
            _size = _currentRoad.GetComponent<BoxCollider2D>().size;

        private void Start() =>
            StartCoroutine(CheckRoad());

        private void OnEnable() =>
            StopCoroutine(CheckRoad());

        private IEnumerator CheckRoad()
        {
            while (isActiveAndEnabled)
            {
                _currentRoad = TrySwitch(_currentRoad);
                InactiveRoad();

                yield return new WaitForSeconds(0.2f);
            }
        }

        private Road TrySwitch(Road target)
        {
            Vector3 point = GetRoadViewportPosition(target.transform.position);

            if (!(point.y <= 0.5f))
                return target;

            _nextRoad = _roadBuilder.Build(_size, _poolObstacles, _poolBoosters);
            _nextRoad.transform.position = target.transform.position +
                                           new Vector3(0, _size.y * target.transform.localScale.y, 0);

            _currentRoad = _nextRoad;
            _queueRoads.Enqueue(target);
            return _nextRoad;
        }

        private void InactiveRoad()
        {
            if (_queueRoads.Count != 2)
                return;

            _previousRoad = _queueRoads.Peek();
            _previousRoad.Clear();
            _previousRoad.gameObject.SetActive(false);
            _queueRoads.Dequeue();
        }

        private Vector3 GetRoadViewportPosition(Vector3 position) =>
            _camera.WorldToViewportPoint(position);
    }
}