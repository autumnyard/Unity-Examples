
namespace AutumnYard.Example1.UI
{
    public sealed class MenuStatus : MenuBase
    {
        public struct Data
        {
            public readonly int MapNumber;
            public readonly string Text;

            public Data(int mapNumber, string text)
            {
                this.MapNumber = mapNumber;
                this.Text = text;
            }
        }

        private Data _data;

        public void Configure(Data data)
        {
            _data = data;
        }
        public override void Clear()
        {
            _data = default;
        }

    }
}
