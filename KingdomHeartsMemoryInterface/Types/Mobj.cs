using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class Mobj : KHMIDataType
    {
        public Mobj(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x0) { }
    }
}
