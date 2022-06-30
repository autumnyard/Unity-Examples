
namespace AutumnYard.Input
{
    public interface IInputProvider<T> where T : struct
    {
        void Subscribe();
        void Unsubscribe();

        T GetInputs();
        void EndFrame();
        void Clear();
    }
}