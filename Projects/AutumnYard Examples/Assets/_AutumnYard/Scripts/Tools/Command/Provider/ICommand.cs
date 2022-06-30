
namespace AutumnYard.Tools.Command
{
    public interface ICommand
    {
        string name { get; }
        void Execute();
    }
}
