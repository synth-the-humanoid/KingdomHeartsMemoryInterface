using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class PartyStatistics : KHMIDataType
    {
        /**
         * Party Statistics represent statistics of party members as they appear in the party menu. 
         * There exists a seperate Statistics structure for all entities that can fight which contains an abridged version of these values
         **/
        public PartyStatistics(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x74)
        {
            
        }

        public byte Level
        {
            get
            {
                return ReadByte(0x0);
            }
            set
            {
                WriteByte(0x0, value);
            }
        }

        public byte CurrentHP
        {
            get
            {
                return ReadByte(0x01);
            }
            set
            {
                WriteByte(0x01, value);
            }
        }

        public byte MaxHP
        {
            get
            {
                return ReadByte(0x02);
            }
            set
            {
                WriteByte(0x02, value);
            }
        }

        public byte CurrentMP
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

        public byte MaxMP
        {
            get
            {
                return ReadByte(0x04);
            }
            set
            {
                WriteByte(0x04, value);
            }
        }

        public byte MaxAP
        {
            get
            {
                return ReadByte(0x05);
            }
            set
            {
                WriteByte(0x05, value);
            }
        }

        public byte Strength
        {
            get
            {
                return ReadByte(0x06);
            }
            set
            {
                WriteByte(0x06, value);
            }
        }

        public byte Defense
        {
            get
            {
                return ReadByte(0x07);
            }
            set
            {
                WriteByte(0x07, value);
            }
        }

        public byte AccessoryCount
        {
            get
            {
                return ReadByte(0x18);
            }
            set
            {
                WriteByte(0x18, value);
            }
        }

        public byte[] Accessories
        {
            get
            {
                return ReadBytes(0x19, AccessoryCount);
            }
            set
            {
                WriteBytes(0x19, value);
            }
        }

        public byte ItemCount
        {
            get
            {
                return ReadByte(0x21);
            }
            set
            {
                WriteByte(0x21, value);
            }
        }

        public byte[] Items
        {
            get
            {
                return ReadBytes(0x22, ItemCount);
            }
            set
            {
                WriteBytes(0x22, value);
            }
        }

        public byte Weapon
        {
            get
            {
                return ReadByte(0x32);
            }
            set
            {
                WriteByte(0x32, value);
            }
        }

        public int EXP
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

        public byte[] Abilities
        {
            get
            {
                return ReadBytes(0x40, 48);
            }
            set
            {
                WriteBytes(0x40, value);
            }
        }
    }
}
