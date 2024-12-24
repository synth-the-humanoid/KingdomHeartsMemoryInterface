namespace KingdomHeartsMemoryInterface.Types
{
    public enum ScriptFunction
    {
        Jump = 0x02,
        Popjz = 0x03, // pop and jump if the value at the top of the stack is zero
        Return = 0x05,
        Push = 0x09
    }
    public class ScriptInstruction : KHMIDataType
    {
        public ScriptInstruction(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x04) { }

        public ScriptInstruction Next
        {
            get
            {
                return new ScriptInstruction(MemoryInterface, Address + 4);
            }
        }

        public byte FunctionID
        {
            get
            {
                return ReadByte(0x03);
            }
            set
            {
                WriteByte(0x03, value);
            }
        }

        public ScriptFunction Function
        {
            get
            {
                return (ScriptFunction)FunctionID;
            }
            set
            {
                FunctionID = (byte)value;
            }
        }

        public int Parameter
        {
            get
            {
                int value = ReadInt(0x0) & 0x00FFFFFF;
                if (value >= 0x800000)
                {
                    value -= 0x1000000;
                }
                return value;
            }
            set
            {
                int pendingValue = value;
                if(pendingValue < 0)
                {
                    pendingValue += 0x1000000;
                }
                byte preservedData = FunctionID;
                WriteInt(0x0, pendingValue & 0x00FFFFFF);
                FunctionID = preservedData;
            }
        }
    }
}
