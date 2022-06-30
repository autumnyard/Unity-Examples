
namespace AutumnYard.Tools.Benchmarking
{
    public interface IBenchmark
    {
        //void Run();
        string Tick(in int frame, in float time, in string message);
        string GetLast();
        string GetAll(in Format format, in bool printSystemInfo);
        void Export(in Format format, in bool printSystemInfo);
    }
}