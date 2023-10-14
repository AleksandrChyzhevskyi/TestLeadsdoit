using System;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Builder
{
    public class RoadBuilder : MonoBehaviour
    {
        [SerializeField] private RoadPool _roadPool;

        private readonly Type[] _obstaclesTypes =
        {
            typeof(Block),
            typeof(Oil),
            typeof(RoadCrack),
        };

        private readonly Type[] _boostersTypes =
        {
            typeof(Coin),
            typeof(CoinMagnet),
            typeof(Heart),
            typeof(Shield),
            typeof(Nitro),
        };


        public Road Build(Vector2 size, PoolObstacles poolObstacles, PoolBoosters poolBoosters)
        {
            Road road = _roadPool.GetFreeRoad();
            GetRandomElement(size, poolObstacles, poolBoosters, road);

            return road;
        }

        private IObstacles GetObstacle(Vector2 size, PoolObstacles poolObstacles, Road road, Type obstacles)
        {
            IObstacles obstacle = poolObstacles.GetDictionaryElement(obstacles);
            obstacle.SetParent(road.transform);
            road.AddObstacles(obstacle);
            obstacle.SetPosition(RandomPosition(size));
            return obstacle;
        }

        private IBooster GetBooster(Vector2 size, PoolBoosters poolBoosters, Road road, Type boosters)
        {
            IBooster booster = poolBoosters.GetDictionaryElement(boosters);
            booster.SetParent(road.transform);
            road.AddBooster(booster);
            booster.SetPosition(RandomPosition(size));
            return booster;
        }

        private Vector3 RandomPosition(Vector2 size)
        {
            float positionY = Random.Range(0, size.y * 0.5f);
            float[] positionsX = { (size.x * 0.2f), -(size.x * 0.2f) };
            float positionX = positionsX[Random.Range(0, positionsX.Length)];
            return new Vector3(positionX, positionY);
        }

        private void GetRandomElement(Vector2 size, PoolObstacles poolObstacles, PoolBoosters poolBoosters,
            Road road)
        {
            int index = Random.Range(0, 2);

            if (index == 1)
                GetRandomBooster(size, poolBoosters, road);
            
            if (index == 0)
                GetRandomObstacle(size, poolObstacles, road);
        }

        private void GetRandomBooster(Vector2 size, PoolBoosters poolBoosters, Road road)
        {
            int index = Random.Range(0, _boostersTypes.Length);
            var currentType = _boostersTypes[index];

            GetBooster(size, poolBoosters, road, currentType);
        }

        private void GetRandomObstacle(Vector2 size, PoolObstacles poolObstacles, Road road)
        {
            int index = Random.Range(0, _obstaclesTypes.Length);
            var currentType = _obstaclesTypes[index];

            GetObstacle(size, poolObstacles, road, currentType);
        }
    }
}