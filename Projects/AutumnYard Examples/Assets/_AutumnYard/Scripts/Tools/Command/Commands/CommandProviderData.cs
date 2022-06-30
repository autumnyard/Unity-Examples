using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.Tools.Command
{
    public struct CommandProviderData
    {
        public string title;
        public ICommandProvider provider;

        public CommandProviderData(string name, ICommandProvider provider)
        {
            this.title = name;
            this.provider = provider;
        }
    }

}