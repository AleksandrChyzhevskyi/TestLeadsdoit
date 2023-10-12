namespace Scripts.Car
{
    public class Healths
    {
        private readonly int _maxPoint;
        private readonly int _minPoint;
        public int Point { get; private set; }

        public Healths(int point, int maxPoint, int minPoint = 0)
        {
            _maxPoint = maxPoint;
            _minPoint = minPoint;
            Point = point;
        }

        public void TakeAway(int value)
        {
            if(value < _minPoint)
                return;

            if (Point - value < _minPoint)
                Point = _minPoint;

            Point -= value;
        }

        public void Add(int value)
        {
            if(value < _maxPoint)
                return;

            if (Point + value > _maxPoint)
                Point = _maxPoint;

            Point += value;
        }
    }
}