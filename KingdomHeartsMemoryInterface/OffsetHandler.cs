using System.Globalization;

namespace KingdomHeartsMemoryInterface
{
    public static class OffsetHandler
    {
        private static Dictionary<string, int> offsetMap = new Dictionary<string, int>();
        private static bool isSetup = false;

        public static void LoadFile(string offsetPath="./offsets.csv")
        {
            if (File.Exists(offsetPath))
            {
                string[] lines = File.ReadAllLines(offsetPath);
                foreach(string line in lines)
                {
                    if(!line.StartsWith("#"))
                    {
                        string[] fields = line.Split(",");
                        if (fields.Length >= 2)
                        {
                            int offset = int.Parse(fields[1], NumberStyles.HexNumber);
                            offsetMap[fields[0]] = offset;
                        }
                    }
                }
                isSetup = true;
            }
        }
        public static int GetOffset(string offsetName)
        {
            if(!isSetup)
            {
                LoadFile();
            }
            if (offsetMap.ContainsKey(offsetName))
            {
                return offsetMap[offsetName];
            }
            return 0;
        }
    }
}
