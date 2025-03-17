using System.Reflection;

namespace KingdomHeartsMemoryInterface
{
    public class KHMI
    {
        private List<KHMIMod> loadedMods = new List<KHMIMod>();
        

        public KHMI(string modsFolder)
        {
            string[] files = Directory.GetFiles(modsFolder, "*.dll");
            foreach (string eachFile in files)
            {
                Assembly currentMod = Assembly.LoadFrom(eachFile);
                Type[] types = currentMod.GetExportedTypes();
                foreach (Type eachType in types)
                {
                    if (eachType == typeof(KHMIMod))
                    {
                        loadedMods.Add((KHMIMod)currentMod.CreateInstance(eachType.Name));
                    }
                }
            }
        }

        public void RunUpdate()
        {
            foreach(KHMIMod eachMod in loadedMods)
            {
                eachMod.Update();
            }
        }
    }
}
