using System;

namespace AutumnYard.Tools.Command
{
    public sealed class CommandActionInt : CommandGenericOne<int>
    {
        private CommandActionInt() { }

        public CommandActionInt(string name, Action<int> toPerform, int value)
        {
            this.name = name;
            enabled = true;
            this.toPerform = toPerform;
            this.value = value;
        }
    }
}
