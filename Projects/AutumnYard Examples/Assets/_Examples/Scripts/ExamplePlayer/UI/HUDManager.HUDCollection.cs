using AutumnYard.Common.UI;

namespace AutumnYard.ExamplePlayer.UI
{
    public sealed partial class HUDManager
    {
        private sealed class HUDCollection
        {
            private HUDBase[] huds;

            public HUDCollection() => huds = new HUDBase[0];
            public HUDCollection(params HUDBase[] huds) => this.huds = huds;

            public void Enter()
            {
                for (int i = huds.Length - 1; i >= 0; i--)
                {
                    //huds[i].HideInstantly();
                    huds[i].Show();
                }
            }
            public void Update()
            {
                for (int i = huds.Length - 1; i >= 0; i--) huds[i].HandleUpdate();
            }
            public void Exit()
            {
                for (int i = huds.Length - 1; i >= 0; i--)
                {
                    //huds[i].ShowInstantly();
                    huds[i].Hide();
                }
            }
        }
    }
}
