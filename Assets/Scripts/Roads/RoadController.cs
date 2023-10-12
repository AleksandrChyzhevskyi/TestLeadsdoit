using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scriots.Roads
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private Road _forstRoad;
        [SerializeField] private List<Road> _roads = new List<Road>();

        private void Start()
        {
            StartRoad();
            Instantiate(_forstRoad, Vector3.zero, Quaternion.identity);
            CreateRoad();
        }

        private void StartRoad()
        {
            foreach (var road in _roads) 
                road.gameObject.SetActive(false);
        }

        public void CreateRoad(BoxCollider2D _boxCollider)
        {
            var lastRoad = _boxCollider.transform.position;
            Vector2 size = _boxCollider.size;

            Road road = GetFreeElement();
            road.transform.position = new Vector3(lastRoad.x, lastRoad.y + (size.y / 2), lastRoad.z);
            
        }

        private Road GetFreeElement()
        {
            foreach (var road in _roads.Where(road => road.isActiveAndEnabled))
            {
                road.gameObject.SetActive(true);
                return road;
            }

            return null;
        }

        private void CreateRoad()
        {
            foreach (var roadPrefab in _roads.Select(Instantiate))
            {
                roadPrefab.gameObject.SetActive(false);
            }
        }
    }
}