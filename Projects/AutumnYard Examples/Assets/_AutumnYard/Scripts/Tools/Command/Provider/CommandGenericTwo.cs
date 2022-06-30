using System;

namespace AutumnYard.Tools.Command
{
    public abstract class CommandGenericTwo<T1, T2> : ICommand
    {
        protected Action<T1, T2> toPerform;
        protected T1 value1;
        protected T2 value2;
        public bool enabled;
        public string name { protected set; get; }

        public void Execute()
        {
            if (enabled)
                toPerform.Invoke(value1, value2);
        }
    }
}
