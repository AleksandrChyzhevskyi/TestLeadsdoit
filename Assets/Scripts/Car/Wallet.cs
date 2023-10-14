namespace Scripts.Car
{
    public class Wallet
    {
        private readonly Stats<Wallet> _wallet;
        private const int DefaultPoint = 1;
        public int CoinsCount => _wallet.Count;

        public Wallet(int count = 0, int maxCount = int.MaxValue, int minCount = 0) => 
            _wallet = new Stats<Wallet>(count, maxCount, minCount);

        public void Add() => 
            _wallet.Add(DefaultPoint);

        public void TakeAway(int value) => 
            _wallet.TakeAway(DefaultPoint);
    }
}