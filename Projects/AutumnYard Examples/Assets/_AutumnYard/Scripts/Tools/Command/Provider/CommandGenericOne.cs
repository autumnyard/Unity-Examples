using System;

namespace AutumnYard.Tools.Command
{
    public abstract class CommandGenericOne<T> : ICommand
    {
        protected Action<T> toPerform;
        protected T value;
        public bool enabled;
        public string name { protected set; get; }

        public void Execute()
        {
            if (enabled)
                toPerform.Invoke(value);
        }
    }
}
