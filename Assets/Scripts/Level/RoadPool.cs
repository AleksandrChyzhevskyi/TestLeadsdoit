using Infrastructure;
using UnityEngine;

namespace Level
{
    public class RoadPool : MonoBehaviour
    {
        [SerializeField] private Road _prefab;
        
        private Pool<Road> _pool;

        private void Start() => 
            _pool = new Pool<Road>(_prefab, transform);

        public Road GetFreeRoad() => 
            _pool.GetFreeElement();
    }

   
}