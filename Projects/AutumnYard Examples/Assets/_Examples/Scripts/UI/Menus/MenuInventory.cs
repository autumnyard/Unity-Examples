
namespace AutumnYard.Example1.UI
{
    public sealed class MenuInventory : MenuBase
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
