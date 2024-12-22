namespace KingdomHeartsMemoryInterface.Types
{
    public enum ScriptEvent
    {
        Examine = 0x9C,
        Hit = 0xA0,
        Talk = 0xA4,
        Touch = 0xA8,
        LockOn = 0xAC,
        Nearby = 0xB4
    }

    public class Script : KHMIDataType
    {
        public static Script[] GetScriptArray(MemoryInterface.MemoryInterface memoryInterface)
        {
            IntPtr scriptArrayBase = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ScriptArrayBase");
            IntPtr scriptArrayCountAddress = memoryInterface.BaseAddress + OffsetHandler.GetOffset("ScriptArrayCount");
            int scriptArrayCount = 0;
            memoryInterface.ReadInt(scriptArrayCountAddress, ref scriptArrayCount);
            Script[] scripts = new Script[scriptArrayCount];
            for(int i = 0; i < scriptArrayCount; i++)
            {
                scripts[i] = new Script(memoryInterface, scriptArrayBase + (i * 0x360));
            }
            return scripts;
        }

        public Script(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x360) { }

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

        public Entity Entity
        {
            get
            {
                return new Entity(MemoryInterface, (IntPtr)ReadLong(0x08));
            }
            set
            {
                WriteLong(0x08, value.EntityPtr);
            }
        }

        public IntPtr CodePtr
        {
            get
            {
                return (IntPtr)ReadLong(0x80);
            }
            set
            {
                WriteLong(0x80, value);
            }
        }

        public int GetEventInstruction(ScriptEvent scriptEvent)
        {
            return ReadInt((int)scriptEvent);
        }

        public void SetEventInstruction(ScriptEvent scriptEvent, int value)
        {
            WriteInt((int)scriptEvent, value);
        }

        public int CurrentInstructionID
        {
            get
            {
                return ReadInt(0x190);
            }
            set
            {
                WriteInt(0x190, value);
            }
        }

        public ScriptInstruction CurrentInstruction
        {
            get
            {
                return GetInstruction(CurrentInstructionID);
            }
        }

        public ScriptInstruction GetInstruction(int instructionID)
        {
            return new ScriptInstruction(MemoryInterface, CodePtr + (instructionID * 4));
        }
    }
}
