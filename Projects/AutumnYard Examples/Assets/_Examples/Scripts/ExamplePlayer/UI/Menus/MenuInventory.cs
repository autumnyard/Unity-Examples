
namespace AutumnYard.ExamplePlayer.UI
{
    public sealed class MenuInventory : Common.UI.MenuBase
    {
        private Inventory _data;

        public void Configure(Inventory data)
        {
            _data = data;
        }
        public override void Clear()
        {
            _data = null;
        }
    }
}
