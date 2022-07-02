 using UnityEngine;

namespace AutumnYard.ExamplePlayer
{
    public enum Item { Apple, Paella, Rocket }
    public sealed class Inventory : SingletonComponent<Inventory>
    {
        public const int MaxItemNumber = 99;

        [NamedArray(typeof(Item))]
        [SerializeField]
        private int[] _items;

        public int[] Items => _items;

        protected override void DoAwake()
        {
            base.DoAwake();

            _items = new int[(typeof(Item)).GetLength()];
        }

        public void AddItem(Item which, int quantity)
        {
            // si tengo 73 y sumo 20, quiero obtener 16
            // 99 - 73 = 16, 20-16

            int initial = _items[(int)which];
            int diff = quantity - (MaxItemNumber - _items[(int)which]);

            _items[(int)which] += quantity;

            if (_items[(int)which] > MaxItemNumber)
                _items[(int)which] = MaxItemNumber;

            Debug.Log($"[Inventory] AddItem: {initial}+{quantity}={_items[(int)which]}, Diff{diff}");

            return;
        }

        public void RemoveItem(Item which, int quantity)
        {
            int initial = _items[(int)which];

            _items[(int)which] -= quantity;

            if (_items[(int)which] < 0)
                _items[(int)which] = 0;

            Debug.Log($"[Inventory] RemoveItem: {initial}-{quantity}={_items[(int)which]}");
        }

    }
}
