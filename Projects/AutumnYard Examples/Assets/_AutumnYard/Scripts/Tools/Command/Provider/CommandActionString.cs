using System;

namespace AutumnYard.Tools.Command
{
    public sealed class CommandActionString : CommandGenericOne<string>
    {
        private CommandActionString() { }

        public CommandActionString(string name, Action<string> toPerform, string value)
        {
            this.name = name;
            enabled = true;
            this.toPerform = toPerform;
            this.value = value;
        }
    }
}