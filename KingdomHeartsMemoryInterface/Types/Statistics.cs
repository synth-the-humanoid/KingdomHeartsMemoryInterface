using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class Statistics : KHMIDataType
    {
        public Statistics(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x100)
        {

        }

        public int CurrentHP
        {
            get
            {
                return ReadInt(0x3C);
            }
            set
            {
                WriteInt(0x3C, value);
            }
        }

        public int MaxHP
        {
            get
            {
                return ReadInt(0x40);
            }
            set
            {
                WriteInt(0x40, value);
            }
        }

        public int CurrentMP
        {
            get
            {
                return ReadInt(0x44);
            }
            set
            {
                WriteInt(0x44, value);
            }
        }

        public int MaxMP
        {
            get
            {
                return ReadInt(0x48);
            }
            set
            {
                WriteInt(0x48, value);
            }
        }

        public int Strength
        {
            get
            {
                return ReadInt(0x4C);
            }
            set
            {
                WriteInt(0x4C, value);
            }
        }

        public int Defense
        {
            get
            {
                return ReadInt(0x50);
            }
            set
            {
                WriteInt(0x50, value);
            }
        }

        public PartyStatistics PartyStatistics
        {
            get
            {
                IntPtr partyStatPointer = (IntPtr)ReadLong(0xC8);
                if(partyStatPointer == IntPtr.Zero)
                {
                    return null;
                }
                return new PartyStatistics(MemoryInterface, partyStatPointer);
            }
        }
    }
}
