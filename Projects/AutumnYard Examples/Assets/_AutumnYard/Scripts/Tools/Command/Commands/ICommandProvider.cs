using System.Collections.Generic;

namespace AutumnYard.Tools.Command
{
    public interface ICommandProvider
    {
        IList<ICommand> GetCommands { get; }
        ICommand this[string which] { get; }
        void Clear();
        void Add(ICommand command);
        void Remove(ICommand command);
    }
}
