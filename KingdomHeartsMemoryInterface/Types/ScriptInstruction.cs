namespace KingdomHeartsMemoryInterface.Types
{
    public enum ScriptFunction
    {
        Jump = 0x02,
        Return = 0x05
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
                byte[] bytes = new byte[4];
                bytes[0] = ReadByte(0x0);
                bytes[1] = ReadByte(0x1);
                bytes[2] = ReadByte(0x2);
                return BitConverter.ToInt32(bytes);
            }
            set
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteByte(0x0, bytes[0]);
                WriteByte(0x01, bytes[1]);
                WriteByte(0x02, bytes[2]);
            }
        }
    }
}
