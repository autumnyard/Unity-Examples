﻿using UnityEngine;

namespace AutumnYard.Common.UI
{
    public sealed class VisibilityHandler_CanvasGroup : VisibilityHandler
    {
        private CanvasGroup _canvasGroup;

        public override bool IsVisible => _canvasGroup.alpha > .97f;

        public VisibilityHandler_CanvasGroup(CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
        }

        public override void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }
        public override void ShowImmediate()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }

        public override void Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }
        public override void HideImmediate()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }

    }
}
