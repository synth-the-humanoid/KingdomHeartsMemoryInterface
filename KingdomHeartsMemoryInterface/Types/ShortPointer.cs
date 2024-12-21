using MemoryInterface;

namespace KingdomHeartsMemoryInterface.Types
{
    public class ShortPointer
    {
        private MemoryInterface.MemoryInterface memoryInterface;
        private IntPtr conversionBaseAddress;
        private int shortValue;

        public ShortPointer(MemoryInterface.MemoryInterface memoryInterface, int shortValue)
        {
            this.memoryInterface = memoryInterface;
            this.conversionBaseAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ShortPointerBase");
            this.shortValue = shortValue;
        }

        public ShortPointer(MemoryInterface.MemoryInterface memoryInterface, IntPtr longValue)
        {
            this.memoryInterface = memoryInterface;
            this.conversionBaseAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ShortPointerBase");
            this.shortValue = ConvertLongToShort(longValue);
        }

        private int ConvertLongToShort(IntPtr longValue)
        {
            if(longValue == IntPtr.Zero)
            {
                return 0;
            }
            IntPtr currentCheckAddress = conversionBaseAddress;
            long checkedValue = 0;
            int i = 0;
            long retVal;
            do
            {
                if(!memoryInterface.ReadLong(currentCheckAddress++, ref checkedValue))
                {
                    throw new InterfaceDisconnectedException();
                }
                if((ulong)checkedValue == ((ulong)longValue & 0xfffffffffe000000))
                {
                    retVal = i << 0x19 | (longValue & 0x1ffffff) | 0x80000000;
                    return (int)retVal;
                }
                if(0x3F < i++)
                {
                    retVal = i * 0x2000000 | (longValue & 0x1ffffff) | 0x80000000;
                    return (int)retVal;
                }
            }
            while ((ulong)checkedValue != 0xffffffffffffffff);

            if(!memoryInterface.WriteLong(conversionBaseAddress + i * 4, (long)((ulong)longValue & 0xfffffffffe000000)))
            {
                throw new InterfaceDisconnectedException();
            }
            retVal = i << 0x19 | (longValue & 0x1ffffff) | 0x80000000;
            return (int)retVal;
        }

        public int ShortValue
        {
            get
            {
                return shortValue;
            }
            set
            {
                shortValue = value;
            }
        }

        public IntPtr LongValue
        {
            get
            {
                if(shortValue == 0)
                {
                    return IntPtr.Zero;
                }
                int offset = ((shortValue & 0x7fffffff) >> 0x19) * 8;
                long basePointer = 0;
                if(memoryInterface.ReadLong(conversionBaseAddress + offset, ref basePointer))
                {
                    return (IntPtr)(basePointer | (shortValue & 0x1ffffff));
                }
                return IntPtr.Zero;
            }
            set
            {
                shortValue = ConvertLongToShort(value);
            }
        }
    }
}
