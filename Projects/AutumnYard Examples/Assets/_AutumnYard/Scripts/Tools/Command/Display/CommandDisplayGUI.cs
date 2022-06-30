using UnityEngine;

namespace AutumnYard.Tools.Command
{
    using AutumnYard.Core.DeveloperTools;

    public sealed class CommandDisplayGUI : MonoBehaviour
    {
        private ICommandProvider _provider;
        private int _index;

        public void Set(int index, ICommandProvider provider)
        {
            _index = index;
            _provider = provider;
        }

        private void OnGUI()
        {
            int i = 0;
            foreach (var command in _provider.GetCommands)
            {
                GUITools.Button(GUITools.CalculateRect(i, _index), command.name, command.Execute);
            }
        }
    }
}
