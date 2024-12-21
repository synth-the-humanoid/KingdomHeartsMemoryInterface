using System.Numerics;

namespace KingdomHeartsMemoryInterface.Types
{
    public abstract class KHMIDataType
    {
        private MemoryInterface.MemoryInterface memoryInterface;
        private IntPtr address = IntPtr.Zero;
        private int structSize;

        public KHMIDataType(MemoryInterface.MemoryInterface memoryInterface, IntPtr address, int structSize)
        {
            this.memoryInterface = memoryInterface;
            this.address = address;
            this.structSize = structSize;
        }

        // below methods read/write data types. throwing generic exception if they run into fault. this means an issue with the memory interface or with the data region being read/written

        protected byte ReadByte(int offset)
        {
            byte result = 0;
            if (memoryInterface.ReadByte(address + offset, ref result)) {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected byte[] ReadBytes(int offset, int count)
        {
            byte[] result = new byte[count];
            if(memoryInterface.ReadBytes(address + offset, count, result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected short ReadShort(int offset)
        {
            short result = 0;
            if (memoryInterface.ReadShort(address + offset, ref result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected int ReadInt(int offset)
        {
            int result = 0;
            if(memoryInterface.ReadInt(address + offset, ref result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected long ReadLong(int offset)
        {
            long result = 0;
            if(memoryInterface.ReadLong(address + offset, ref result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected float ReadFloat(int offset)
        {
            float result = 0;
            if(memoryInterface.ReadFloat(address + offset, ref result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected double ReadDouble(int offset)
        {
            double result = 0;
            if (memoryInterface.ReadDouble(address + offset, ref result))
            {
                return result;
            }
            throw new InterfaceDisconnectedException();
        }

        protected string ReadString(int offset, int maxSize)
        {
            string data = "";
            for(int i = 0; i < maxSize; i++)
            {
                byte nextByte = ReadByte(offset + i);
                if (nextByte == 0)
                {
                    break;
                }
                data = data + (char)nextByte;
            }
            return data;
        }

        protected Vector3 ReadVector3(int offset)
        {
            return new Vector3(ReadFloat(offset), ReadFloat(offset + 4), ReadFloat(offset + 8));
        }

        protected Quaternion ReadQuaternion(int offset)
        {
            return new Quaternion(ReadFloat(offset + 8), ReadFloat(offset + 16), ReadFloat(offset + 24), ReadFloat(offset));
        }

        protected void WriteByte(int offset, byte value)
        {
            if(!memoryInterface.WriteByte(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteBytes(int offset, byte[] value)
        {
            if(!memoryInterface.WriteBytes(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteShort(int offset, short value)
        {
            if(!memoryInterface.WriteShort(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteInt(int offset, int value)
        {
            if(!memoryInterface.WriteInt(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteLong(int offset, long value)
        {
            if(!memoryInterface.WriteLong(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteFloat(int offset, float value)
        {
            if(!memoryInterface.WriteFloat(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteDouble(int offset, double value)
        {
            if (!memoryInterface.WriteDouble(address + offset, value))
            {
                throw new InterfaceDisconnectedException();
            }
        }

        protected void WriteString(int offset, string value)
        {
            char[] chars = value.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for(int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }

            WriteBytes(offset, bytes);
        }

        protected void WriteVector3(int offset, Vector3 value)
        {
            WriteFloat(offset, value.X);
            WriteFloat(offset + 4, value.Y);
            WriteFloat(offset + 8, value.Z);
        }

        protected void WriteQuaternion(int offset, Quaternion value)
        {
            WriteFloat(offset, value.W);
            WriteFloat(offset + 8, value.X);
            WriteFloat(offset + 16, value.Y);
            WriteFloat(offset + 24, value.Z);
        }

        protected MemoryInterface.MemoryInterface MemoryInterface
        {
            get
            {
                return memoryInterface;
            }
        }

        protected IntPtr Address
        {
            get
            {
                return address;
            }
        }

        protected int Size
        {
            get
            {
                return structSize;
            }
        }
    }
}
