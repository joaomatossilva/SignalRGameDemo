using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SignalrDemo1
{
    public class GameCore
    {
        private ConcurrentDictionary<string, Entry> results = new ConcurrentDictionary<string, Entry>();
        public string RoundValue { get; private set; }
        public DateTime RoundStart { get; private set; }
        private static readonly string[] AllValues = { "Red", "Blue", "Yellow", "Green" };
        private static readonly Random Rng = new Random();

        private GameCore()
        {
            Reset();
        }

        public static GameCore Instance { get; } = new GameCore();

        public void Reset()
        {
            results.Clear();
            RoundValue = AllValues[Rng.Next() % AllValues.Length];
            RoundStart = DateTime.Now;
        }

        public void Submit(string name, string value)
        {
            DateTime now = DateTime.UtcNow;

            var isCorrect = RoundValue.Equals(value, StringComparison.OrdinalIgnoreCase);
            var entry = new Entry
            {
                Correct = isCorrect,
                Name = name,
                Value = value,
                Time = now.Subtract(RoundStart)
            };
            results.AddOrUpdate(name, entry, (_, existing) => existing.Time > entry.Time ? entry : existing);
        }

        public IEnumerable<Entry> GetScore()
        {
            return results.ToArray()
                .Select(x => x.Value)
                .OrderByDescending(x => x.Correct)
                .ThenBy(x => x.Time);
        }

        public struct Entry
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public bool Correct { get; set; }
            public TimeSpan Time { get; set;}
        }
    }
}
