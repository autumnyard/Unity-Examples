using System;

namespace AutumnYard.Tools.Command
{
    public struct CommandAction : ICommand
    {
        readonly private Action toPerform;
        public bool enabled;
        public string name { private set; get; }

        public CommandAction(string name, Action toPerform)
        {
            this.name = name;
            enabled = true;
            this.toPerform = toPerform;
        }

        public void Execute()
        {
            if (enabled)
                toPerform.Invoke();
        }

    }
}
