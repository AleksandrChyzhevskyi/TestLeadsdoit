using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Level
{
    public class PoolObstacles : MonoBehaviour
    {
        [SerializeField] private Block _block;
        [SerializeField] private Oil _oil;
        [SerializeField] private RoadCrack _roadCrack;

        private Dictionary<Type, Func<IObstacles>> _dictionary;

        private Pool<Block> _blockPool;
        private Pool<Oil> _oilPool;
        private Pool<RoadCrack> _roadCrackPool;

        private void Awake()
        {
            _blockPool = CreatePool(_block);
            _oilPool = CreatePool(_oil);
            _roadCrackPool = CreatePool(_roadCrack);

            _dictionary = new Dictionary<Type, Func<IObstacles>>()
            {
                [typeof(Block)] = GetFreeBlock,
                [typeof(Oil)] = GetFreeOil,
                [typeof(RoadCrack)] = GetFreeRoadCrack,
            };
        }

        private Pool<T> CreatePool<T>(T prefab) where T : MonoBehaviour =>
          new Pool<T>(prefab, transform);

        private Block GetFreeBlock() =>
            _blockPool.GetFreeElement();

        private Oil GetFreeOil() =>
            _oilPool.GetFreeElement();

        private RoadCrack GetFreeRoadCrack() =>
            _roadCrackPool.GetFreeElement();

        public IObstacles GetDictionaryElement(Type type)
        {
            if (!_dictionary.ContainsKey(type))
                return null; 
                
            IObstacles obstacle = _dictionary[type].Invoke();
            obstacle.ObstacleCollected += OnObstacleCollected;
            return obstacle;
        }

        public IObstacles GetDictionaryElement<T>() where T: class, IObstacles => 
            GetDictionaryElement(typeof(T));
        
        private void OnObstacleCollected(IObstacles obstacle)
        {
            obstacle.ObstacleCollected -= OnObstacleCollected;
            obstacle.SetParent(transform);
            obstacle.Inactive();
        }
    }
} 