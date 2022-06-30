
namespace AutumnYard.Plugins.CSVReader
{
    public static class CSVTools
    {
        public static void Deserialize<T>(in string raw, out T[] output)
        {
            output = CSVSerializer.Deserialize<T>(raw);
        }
    }
}
