
namespace AutumnYard.Tools.IO
{
    public interface IPersistence
    {
        void Save();
        void Load();
        void Clear();
    }
}