using MemoryInterface;
using System.Numerics;

namespace KingdomHeartsMemoryInterface.Types
{
    public class Entity : KHMIDataType
    {
        public static Entity[] GetEntityArray(MemoryInterface.MemoryInterface memoryInterface)
        {
            IntPtr baseAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("EntityArrayBase");
            IntPtr endAddressPtr = memoryInterface.BaseAddress + OffsetHandler.GetOffset("EntityArrayEndPtr");
            long endAddressLong = 0;
            if(memoryInterface.ReadLong(endAddressPtr, ref endAddressLong))
            {
                long variance = endAddressLong - (long)baseAddress;
                int size = (int)(variance / 0x4B0);
                Entity[] entities = new Entity[size];
                for(int i = 0; i < size; i++)
                {
                    entities[i] = new Entity(memoryInterface, baseAddress + (i * 0x4B0));
                }
                return entities;
            }
            return new Entity[0];
        }

        public Entity(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x4B0)
        {
            
        }

        public IntPtr EntityPtr
        {
            get
            {
                return Address;
            }
        }

        public int ScriptIDCode
        {
            get
            {
                return ReadInt(0x0);
            }
            set
            {
                WriteInt(0x0, value);
            }
        }

        public Vector3 Position
        {
            get
            {
                return ReadVector3(0x10);
            }
            set
            {
                WriteVector3(0x10, value);
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return ReadQuaternion(0x20);
            }
            set
            {
                WriteQuaternion(0x20, value);
            }
        }

        

        public Statistics Statistics
        {
            get
            {
                IntPtr targetAddress = new ShortPointer(MemoryInterface, ReadInt(0x6C)).LongValue;
                return new Statistics(MemoryInterface, targetAddress);
            }
        }

        public Actor Actor
        {
            get
            {
                IntPtr targetAddress = new ShortPointer(MemoryInterface, ReadInt(0x130)).LongValue;
                return new Actor(MemoryInterface, targetAddress);
            }
        }

        public float Red
        {
            get
            {
                return ReadFloat(0xA0);
            }
            set
            {
                WriteFloat(0xA0, value);
            }
        }
        public float Green
        {
            get
            {
                return ReadFloat(0xA4);
            }
            set
            {
                WriteFloat(0xA4, value);
            }
        }
        public float Blue
        {
            get
            {
                return ReadFloat(0xA8);
            }
            set
            {
                WriteFloat(0xA8, value);
            }
        }
        public float Alpha
        {
            get
            {
                return ReadFloat(0xAC);
            }
            set
            {
                WriteFloat(0xAC, value);
            }
        }
    }
}
