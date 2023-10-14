using UnityEngine;

namespace Scripts.Car
{
    [System.Serializable]
    public class CarStats
    {
        //Авто свойства - булка в шите неуязвимость 
        public readonly Healths Healths = new Healths();
        public readonly Wallet WalletPlayer = new Wallet();
        
        public const float DefaultMaxSpeed = 5f;
        public const float GasMultiplayer = 1f;
        public const int BoosterEffectSecond = 15;
        public const int ObstaclesEffectSecond = 10;
        
        public float MaxSpeed = 5f;
        public Vector2 RotateDirection = Vector2.zero;
        
        public int PointHealths => Healths.HealthsCount;
        public int CountCoins => WalletPlayer.CoinsCount;
    }
}