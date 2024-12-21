namespace KingdomHeartsMemoryInterface.Types
{
    public class KHString : KHMIDataType
    {
        private static Dictionary<byte, string> cipher;
        public KHString(MemoryInterface.MemoryInterface memoryInterface, IntPtr address) : base(memoryInterface, address, 0x0)
        {
            if(cipher == null)
            {
                SetupCipher();
            }
        }

        private static void SetupCipher()
        {
            cipher = new Dictionary<byte, string>();
            cipher[0x0] = "\0";
            cipher[0x1] = " ";
            cipher[0x2] = "\n";
            cipher[0x6E] = "-";
            byte i = 0x21;
            int i2 = 0;
            string numbers = "0123456789";
            while(i < 0x2B)
            {
                cipher[i] = numbers.Substring(i2++, 1);
                i++;
            }
            i2 = 0;
            string lettersAndFirstPunc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!?&%+-x/*.,";
            while(i < 0x65)
            {
                cipher[i] = lettersAndFirstPunc.Substring(i2++, 1);
                i++;
            }
        }

        public static string ConvertBytesToString(byte[] data)
        {
            if (cipher == null)
            {
                SetupCipher();
            }
            string retVal = "";
            foreach(byte eachByte in data)
            {
                if(cipher.ContainsKey(eachByte))
                {
                    retVal += cipher[eachByte];
                }
            }
            return retVal;
        }

        public static byte[] ConvertStringToBytes(string data)
        {
            List<byte> bytes = new List<byte>();
            Dictionary<string, byte> flipped = new Dictionary<string, byte>();
            foreach (char eachChar in data.ToCharArray())
            {
                string current = string.Format("{0}", eachChar);
                if (flipped.ContainsKey(current))
                {
                    bytes.Add(flipped[current]);
                }
            }
            return bytes.ToArray();
        }

        public string String
        {
            get
            {
                List<byte> bytes = new List<byte>();
                int i = 0;
                byte currentByte;
                do
                {
                    currentByte = ReadByte(i++);
                    bytes.Add(currentByte);
                }
                while (currentByte != 0x0);
                return ConvertBytesToString(bytes.ToArray());
            }
            set
            {
                WriteBytes(0x0, ConvertStringToBytes(value));
            }
        }
    }
}
