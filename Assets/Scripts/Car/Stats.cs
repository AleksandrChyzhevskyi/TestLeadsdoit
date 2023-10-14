namespace Scripts.Car
{
    public class Stats <T> where T: class
    {
        private readonly int _maxPoint;
        private readonly int _minPoint;
        public int Count { get; private set; }

        public Stats(int count, int maxPoint, int minPoint = 0)
        {
            _maxPoint = maxPoint;
            _minPoint = minPoint;
            Count = count;
        }

        public void TakeAway(int value)
        {
            if(value < _minPoint)
                return;

            if (Count - value < _minPoint)
                Count = _minPoint;

            Count -= value;
        }

        public void Add(int value)
        {
            if(value < _maxPoint)
                return;

            if (Count + value > _maxPoint)
                Count = _maxPoint;

            Count += value;
        }
    }
}