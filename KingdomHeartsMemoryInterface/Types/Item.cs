using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class Item : KHMIDataType
    {
        public static Item[] GetItemArray(MemoryInterface.MemoryInterface memoryInterface)
        {
            Item[] items = new Item[0xFF];
            for(int i = 0; i < items.Length; i++)
            {
                items[i] = GetItemFromID(memoryInterface, (byte)i);
            }
            return items;
        }

        public static Item GetItemFromID(MemoryInterface.MemoryInterface memoryInterface, byte itemID)
        {
            IntPtr baseAddressPtr = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ItemArrayBasePtr");
            long baseAddress = 0;
            itemID--;
            if(memoryInterface.ReadLong(baseAddressPtr, ref baseAddress))
            {
                return new Item(memoryInterface, (IntPtr)baseAddress + (itemID * 0x14));
            }
            return null;
        }


        public Item(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x14) { }

        public Action Action
        {
            get
            {
                return Action.GetActionFromID(MemoryInterface, ReadShort(0x0));
            }
        }

        public KHString Description
        {
            get
            {
                IntPtr descAddress = new ShortPointer(MemoryInterface, ReadInt(0x0C)).LongValue;
                return new KHString(MemoryInterface, descAddress);
            }
        }
    }
}
