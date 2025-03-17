namespace KingdomHeartsMemoryInterface.Types
{
    public class KHStringBlock : KHMIDataType
    {
        private List<KHString> strings = new List<KHString>();

        public KHStringBlock(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x0)
        {
            int count = ReadInt(0x0);
            int offset = 0;
            for(int i = 0; i < count; i++)
            {
                KHString current = new KHString(memoryInterface, address + offset);
                strings.Add(current);
                offset += current.Bytes.Length;
            }
        }

        public KHString GetStringAtIndex(int index)
        {
            return strings[index];
        }

        public KHString[] GetAll()
        {
            return strings.ToArray();
        }
    }
}
