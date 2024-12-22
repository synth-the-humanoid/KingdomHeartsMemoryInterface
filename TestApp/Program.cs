using MemoryInterface;
using KingdomHeartsMemoryInterface;
using KingdomHeartsMemoryInterface.Types;

MemoryInterface.MemoryInterface memoryInterface = new MemoryInterface.MemoryInterface("KINGDOM HEARTS FINAL MIX");
OffsetHandler.LoadFile("./offsets.csv");

Script selectedScript = null;
int selectionIndex = 0;
string userInput = "";
do
{
    Script[] loadedScripts = Script.GetScriptArray(memoryInterface);
    Console.WriteLine("Script Count: {0:D}", loadedScripts.Length);
    userInput = Console.ReadLine();
    if(!int.TryParse(userInput, out selectionIndex) && selectedScript != null)
    {
        string[] fields = userInput.Split(" ");
        switch(fields[0])
        {
            case "list":
                int instructionStart = 0;
                int instructionEnd = 0;
                if (fields.Length == 3 && int.TryParse(fields[1], out instructionStart) && int.TryParse(fields[2], out instructionEnd))
                {
                    for(int i = instructionStart; i <= instructionEnd; i++)
                    {
                        ScriptInstruction currentInstruction = selectedScript.GetInstruction(i);
                        Console.WriteLine("Instruction ID: {0:X}\tFunction ID: {1:X}\tParameter: {2:D}", i, currentInstruction.FunctionID, currentInstruction.Parameter);
                    }
                }
                else
                {
                    Console.WriteLine("Format: list <instructionStart> <instructionEnd>");
                }
                break;
            default:
                Console.WriteLine("Unrecognized command.");
                break;
        }
    }
    else
    {
        selectedScript = loadedScripts[Math.Clamp(selectionIndex, 0, loadedScripts.Length - 1)];
        string entityName = "None";
        if (selectedScript.Entity.EntityPtr != 0)
        {
            entityName = selectedScript.Entity.Actor.Name;
        }
        ScriptInstruction currentInstruction = selectedScript.CurrentInstruction;
        Console.WriteLine("Script ID: {0:D}\nEntity: {1}\nScript ID Code: {2:X}\nCurrent Instruction: {3:X}\nCurrent Function: {4:X}\nCurrent Parameter: {5:D}\n", selectionIndex, entityName, selectedScript.ScriptIDCode, selectedScript.CurrentInstructionID, currentInstruction.FunctionID, currentInstruction.Parameter);
    }
}
while (userInput != "exit");

memoryInterface.Close();