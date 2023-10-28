namespace Common.Draws
{
    public class Package
    {
        public Package(int from, int to, int id)
        {
            From = from;
            To = to;
            Progress = 0;
            Position = from;
            Id = id;
            LastPosition = from;
            WasCopy = false;
        }

        public Package(int from, int to, int id, int next, int pos, int count, int lastPosition)
        {
            From = from;
            To = to;
            Progress = 0;
            NextTarget = next;
            Position = pos;
            Count = count;
            Id = id;
            LastPosition = lastPosition;
            WasCopy = true;
        }

        public int From { get; set; }

        public int To { get; set; }

        public int Progress { get; set; } //От 0 до 100 % пройденного пути до вершины

        public int NextTarget { get; set; }

        public int Position { get; set; } //Пакет сейчас тут

        public int Count { get; set; } //Количество переходов

        public int Id { get; set; } //Для валовой маршрутизации (дублирование пакетов)

        public int LastPosition { get; set; } //Для валовой маршрутизации (дублирование пакетов)

        public bool WasCopy { get; set; }
    }
}