namespace DeX
{
    public class QueueTest
    {
        private readonly int _threshold;
        public Queue<string> PeopleQueue = new Queue<string>();
        public delegate void EventHandler(string message);
        public event EventHandler ThresholdReached;
        private int Counter = 0;
        public QueueTest(int threshold)
        {
            _threshold = threshold;
        }
        public void FilQueue(List<string> persons)
        {
            foreach (string item in persons)
            {
                Counter++;
                if (Counter > _threshold)
                {
                    ThresholdReached?.Invoke("Threshold reached");
                    break;
                }
                PeopleQueue.Enqueue(item);
                Console.WriteLine($"{item} added");
                Thread.Sleep(1000);
            }
        }
        public void PrintPeople()
        {
            foreach (var item in PeopleQueue)
            {
                Console.WriteLine(item);
                Thread.Sleep(1000);
            }
            ThresholdReached?.Invoke("Queue is empty");
        }
    }
}
