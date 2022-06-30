using System.Collections.Generic;

namespace AutumnYard.Tools.Command
{
    [System.Serializable]
    public class CommandProvider : ICommandProvider
    {
        /*[Show]*/
        private IList<ICommand> commandsList;
        /*[Show]*/
        private Dictionary<string, ICommand> commands;

        public IList<ICommand> GetCommands => commandsList;
        public ICommand this[string which] => commands[which];


        public CommandProvider()
        {
            Clear();
        }

        internal void Initialize()
        {
            Clear();
        }

        public void Clear()
        {
            commandsList = new List<ICommand>();
            commands = new Dictionary<string, ICommand>();
        }

        public void Add(ICommand command)
        {
            commandsList.Add(command);
            commands.Add(command.name, command);
        }

        public void Remove(ICommand command)
        {
            commandsList.Remove(command);
            commands.Remove(command.name);
        }
    }
}
