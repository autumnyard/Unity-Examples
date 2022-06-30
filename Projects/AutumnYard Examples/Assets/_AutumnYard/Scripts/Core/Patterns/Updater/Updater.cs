
namespace AutumnYard.Tools.Updater
{
    public sealed class Updater
    {
        public interface IUpdater<T, Tdata> where T : class where Tdata : struct
        {
            void Enter(in T client) { }
            void Update(ref T client, ref Tdata updateData) { }
            void Exit(in T client) { }
        }

        public static class Tools
        {
            public static void Update<T, Tdata>(IUpdater<T, Tdata>[] states, in int currentValue, ref Tdata data, T client)
                 where T : class
                 where Tdata : struct
            {
                states[currentValue].Update(ref client, ref data);
            }
        }
    }
}