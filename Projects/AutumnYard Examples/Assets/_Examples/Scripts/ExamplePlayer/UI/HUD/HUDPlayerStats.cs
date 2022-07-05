using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using AutumnYard.Common.UI;
using AutumnYard.Core.Display.UI;

namespace AutumnYard.ExamplePlayer.UI
{
    public sealed class HUDPlayerStats : HUDBase
    {
        [SerializeField] private TextDisplay text;
        [SerializeField] private ExamplePlayerDirector director;

        private void OnValidate()
        {
            if (text == null) text = GetComponentInChildren<TextDisplay>();
            if (director == null) director = FindObjectOfType<ExamplePlayerDirector>();
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

        private void SetGameState(ExamplePlayerDirector.State newState)
        {
            text.Set($"Player: <b>{newState}</b>");
        }
    }
}
