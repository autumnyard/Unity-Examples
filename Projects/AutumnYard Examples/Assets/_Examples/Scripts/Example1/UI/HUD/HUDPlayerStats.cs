using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using AutumnYard.Core.Display.UI;

namespace AutumnYard.Example1.UI
{
    public sealed class HUDPlayerStats : HUDBase
    {
        [SerializeField] private TextDisplay text;
        [SerializeField] private Example1Director director;

        private void OnValidate()
        {
            if (text == null) text = GetComponentInChildren<TextDisplay>();
            if (director == null) director = FindObjectOfType<Example1Director>();
        }

        protected override void Configure()
        {
            SetGameState(director.CurrentState);
            director.onChangeState += SetGameState;
        }
        protected override void Clear()
        {
            director.onChangeState -= SetGameState;
        }

        private void SetGameState(Example1Director.State newState)
        {
            text.Set($"Player: <b>{newState}</b>");
        }
    }
}
