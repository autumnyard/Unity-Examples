
namespace AutumnYard.Input
{
    public interface IInputHandler<T> where T : struct
    {
        void UpdateWithInputs(in T inputs);
        void Clear();
    }
}
