using System;

namespace AutumnYard.Tools.Command
{
    public class CommandActionIntString : CommandGenericTwo<int, string>
    {
        private CommandActionIntString() { }

        public CommandActionIntString(string name, Action<int, string> toPerform, int value1, string value2)
        {
            this.name = name;
            enabled = true;
            this.toPerform = toPerform;
            this.value1 = value1;
            this.value2 = value2;
        }
    }
}
