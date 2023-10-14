namespace Scripts.Car
{
    public class Healths
    {
        private readonly Stats<Healths> _healths;
        private const int DefaultPoint = 25;

        public int HealthsCount => _healths.Count;

        public Healths(int count = 100, int mixPoint = 100, int maxPoint = 0) =>
            _healths = new Stats<Healths>(count, maxPoint, mixPoint);

        public void Add() => 
            _healths.Add(DefaultPoint);

        public void TakeAway() =>
            _healths.TakeAway(DefaultPoint);

        public void SetHealths(int point) =>
            _healths.Add(point);


    }
}