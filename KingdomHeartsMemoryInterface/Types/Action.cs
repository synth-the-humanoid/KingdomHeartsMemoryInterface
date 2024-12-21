namespace KingdomHeartsMemoryInterface.Types
{
    public class Action : KHMIDataType
    {
        private short actionID;

        public static Action GetActionFromID(MemoryInterface.MemoryInterface memoryInterface, short actionID)
        {
            IntPtr baseAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ActionArrayBase");
            return new Action(memoryInterface, baseAddress + (actionID * 0x4));
        }

        public Action(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x4)
        {
            IntPtr baseAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ActionArrayBase");
            actionID = (short)((address - baseAddress) / 0x4);
        }

        public short ActionID
        {
            get
            {
                return actionID;
            }
        }

        public KHString Name
        {
            get
            {
                IntPtr stringAddress = new ShortPointer(MemoryInterface, ReadInt(0x0)).LongValue;
                return new KHString(MemoryInterface, stringAddress);
            }
        }
    }
}
