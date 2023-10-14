using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Level
{
    public class PoolBoosters : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private CoinMagnet _coinMagnet;
        [SerializeField] private Heart _heart;
        [SerializeField] private Nitro _nitro;
        [SerializeField] private Shield _shield;

        private Dictionary<Type, Func<IBooster>> _dictionary;
        private Pool<Coin> _coinsPool;
        private Pool<CoinMagnet> _coinMagnetPool;
        private Pool<Heart> _heartPool;
        private Pool<Nitro> _nitroPool;
        private Pool<Shield> _ShieldPool;

        private void Start()
        {
            _coinsPool = CreatePool(_coin);
            _heartPool = CreatePool(_heart);
            _nitroPool = CreatePool(_nitro);
            _ShieldPool = CreatePool(_shield);
            _coinMagnetPool = CreatePool(_coinMagnet);

            _dictionary = new Dictionary<Type, Func<IBooster>>()
            {
                [typeof(Coin)] = GetFreeCoin,
                [typeof(CoinMagnet)] = GetFreeCoinMagnet,
                [typeof(Shield)] = GetFreeShield,
                [typeof(Nitro)] = GetFreeNitro,
                [typeof(Heart)] = GetFreeHeart,
            };
        }

        private Pool<T> CreatePool<T>(T prefab) where T : MonoBehaviour =>
            new Pool<T>(prefab, transform);

        private Coin GetFreeCoin() =>
            _coinsPool.GetFreeElement();

        private CoinMagnet GetFreeCoinMagnet() =>
            _coinMagnetPool.GetFreeElement();

        private Nitro GetFreeNitro() =>
            _nitroPool.GetFreeElement();

        private Heart GetFreeHeart() =>
            _heartPool.GetFreeElement();

        private Shield GetFreeShield() =>
            _ShieldPool.GetFreeElement();

        public IBooster GetDictionaryElement(Type type)
        {
             if(!_dictionary.ContainsKey(type))
               return null;

             IBooster booster = _dictionary[type].Invoke();
             booster.BoostCollected += OnBoosterCollected;
             return booster;
        }

        public IBooster GetDictionaryElement<T>() where T : class, IBooster =>
            GetDictionaryElement(typeof(T));

        private void OnBoosterCollected(IBooster booster)
        {
            booster.BoostCollected -= OnBoosterCollected;
            booster.SetParent(transform);
            booster.Inactive();
        }
    }
}