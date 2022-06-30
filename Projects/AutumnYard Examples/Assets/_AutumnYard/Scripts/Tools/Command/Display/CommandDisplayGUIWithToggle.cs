using UnityEngine;

namespace AutumnYard.Tools.Command
{
    using AutumnYard.Core.DeveloperTools;

    public sealed class CommandDisplayGUIWithToggle : MonoBehaviour
    {
        private ICommandProvider provider;
        private int _index;
        private bool _show = true;
        private string _title;

        public void Set(int index, CommandProviderData data)
        {
            _index = index;
            _title = data.title;
            provider = data.provider;
        }

        private void OnGUI()
        {
            int i = 0;

            _show = GUI.Toggle(GUITools.CalculateRect(i, _index), _show, _title);

            if (!_show) return;

            foreach (var command in provider.GetCommands)
            {
                i++;
                GUITools.Button(GUITools.CalculateRect(i, this._index), command.name, command.Execute);
            }
        }
    }
}
