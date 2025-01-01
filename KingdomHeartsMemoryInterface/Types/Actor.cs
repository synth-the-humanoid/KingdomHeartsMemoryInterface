using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class Actor : KHMIDataType
    {
        public Actor(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x78)
        {
            
        }

        public int ScriptIDCode
        {
            get
            {
                return ReadInt(0x4);
            }
            set
            {
                WriteInt(0x4, value);
            }
        }

        public string Name
        {
            get
            {
                return ReadString(0x68, 16);
            }
            set
            {
                WriteString(0x68, value);
            }
        }

        public Mobj Mobj
        {
            get
            {
                IntPtr mobjAddress = new ShortPointer(MemoryInterface, ReadInt(0x8)).LongValue;
                return new Mobj(MemoryInterface, mobjAddress);
            }
        }

        public string MDLS
        {
            get
            {
                IntPtr target = new ShortPointer(MemoryInterface, ReadInt(0x60)).LongValue;
                string data = "";
                MemoryInterface.ReadString(target, ref data);
                return data;
            }
            set
            {
                IntPtr target = new ShortPointer(MemoryInterface, ReadInt(0x60)).LongValue;
                MemoryInterface.WriteString(target, value);
            }
        }

        public string MSET
        {
            get
            {
                IntPtr target = new ShortPointer(MemoryInterface, ReadInt(0x64)).LongValue;
                string data = "";
                MemoryInterface.ReadString(target, ref data);
                return data;
            }
            set
            {
                IntPtr target = new ShortPointer(MemoryInterface, ReadInt(0x64)).LongValue;
                MemoryInterface.WriteString(target, value);
            }
        }
    }
}
