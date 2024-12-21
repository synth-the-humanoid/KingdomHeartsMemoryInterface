using MemoryInterface;
using KingdomHeartsMemoryInterface;
using KingdomHeartsMemoryInterface.Types;

MemoryInterface.MemoryInterface memoryInterface = new MemoryInterface.MemoryInterface("KINGDOM HEARTS FINAL MIX");
OffsetHandler.LoadFile("./offsets.csv");

Script[] loadedScripts = Script.GetScriptArray(memoryInterface);
string userInput = "";
bool doAutoloop = false;
int index = 0;
Entity currentEntity = null;
Script current = null;

while (userInput != "exit")
{
    if(doAutoloop)
    {
        Thread.Sleep(1000);
        if(Console.KeyAvailable)
        {
            doAutoloop = false;
            continue;
        }
    }
    else
    {
        Console.WriteLine("Scripts: {0:D}", loadedScripts.Length);
        userInput = Console.ReadLine();
    }
    if(doAutoloop || userInput == "" || int.TryParse(userInput, out index))
    {
        current = loadedScripts[index];
        string entityName = "None";
        if (current.Entity.EntityPtr != 0)
        {
            if(currentEntity != null)
            {
                currentEntity.Green = 1f;
                currentEntity.Blue = 1f;
            }
            currentEntity = current.Entity;
            entityName = currentEntity.Actor.Name;
            currentEntity.Green = 0f;
            currentEntity.Blue = 0f;
        }
        ScriptInstruction currentInstruction = current.GetInstruction(current.CurrentInstruction);
        string data = string.Format("Script ID: {0:D}\nScript ID Code: {1:X}\nEntity: {2}\nCurrent Instruction: {3:X}\nCurrent Function: {4:X}\nCurrent Parameter: {5:X}", index, current.ScriptIDCode, entityName, current.CurrentInstruction, currentInstruction.FunctionID, currentInstruction.Parameter);
        Console.WriteLine(data);
    }
    else
    {
        if(userInput != "exit")
        {
            if (userInput == "loop")
            {
                doAutoloop = true;
            }
            else if(userInput.StartsWith("set"))
            {
                string[] fields = userInput.Split(" ");
                if (fields[1] == "entity")
                {
                    string actorName = fields[2];
                    foreach(Entity eachEntity in Entity.GetEntityArray(memoryInterface))
                    {
                        if(eachEntity.Actor.Name == actorName)
                        {
                            current.Entity = eachEntity;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unrecognized command");
                }
            }
            else
            {
                Console.WriteLine("Input a number only.");
            }
        }
    }
}

memoryInterface.Close();