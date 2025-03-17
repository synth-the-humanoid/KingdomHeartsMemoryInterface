using MemoryInterface;
using KingdomHeartsMemoryInterface;
using KingdomHeartsMemoryInterface.Types;

MemoryInterface.MemoryInterface memoryInterface = new MemoryInterface.MemoryInterface("KINGDOM HEARTS FINAL MIX");
OffsetHandler.LoadFile("./offsets.csv");

IntPtr address = memoryInterface.BaseAddress + OffsetHandler.GetOffset("TextBoxBase");

Item[] items = Item.GetItemArray(memoryInterface);
foreach(Item eachItem in items)
{
    Console.WriteLine(string.Format("{0}\n{1}\n\n", eachItem.Action.Name.String, eachItem.ShopDescription.String));
}


memoryInterface.Close();