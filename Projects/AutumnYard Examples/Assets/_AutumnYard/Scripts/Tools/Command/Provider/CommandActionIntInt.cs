using System;

namespace AutumnYard.Tools.Command
{
    public class CommandActionIntInt : CommandGenericTwo<int, int>
    {
        private CommandActionIntInt() { }

        public CommandActionIntInt(string name, Action<int, int> toPerform, int value1, int value2)
        {
            this.name = name;
            enabled = true;
            this.toPerform = toPerform;
            this.value1 = value1;
            this.value2 = value2;
        }
    }
}
