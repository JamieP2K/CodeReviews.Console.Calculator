using Newtonsoft.Json;
using System;
using System.IO;

namespace CalculatorLibrary
{
    public static class UsageCounter
    {
        private class UsageData
        {
            public int Usage { get; set; } = 0;
        }

        private static readonly string filePath =
            Path.Combine(AppContext.BaseDirectory, "usage.json");
        private static readonly object padlock = new object();
        private static UsageData data = Load();

        public static void IncreaseUsageCounter()
        {
            lock (padlock)
            {
                data.Usage++;
                Save();
            }
        }

        public static int ReturnUsageCounter()
        {
            lock (padlock)
            {
                return data.Usage;
            }
        }

        private static UsageData Load()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    return JsonConvert.DeserializeObject<UsageData>(File.ReadAllText(filePath))
                           ?? new UsageData();
                }
                catch (JsonException) { }
            }
            return new UsageData();
        }

        private static void Save()
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}