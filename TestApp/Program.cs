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
    if(!int.TryParse(userInput, out selectionIndex) && (selectedScript != null || userInput == "help"))
    {
        if(selectedScript != null)
        {
            userInput = userInput.Replace("current", string.Format("{0:D}", selectedScript.CurrentInstructionID));
        }
        string[] fields = userInput.Split(" ");
        int instructionStart = 0;
        int instructionEnd = 0;
        switch (fields[0])
        {
            case "help":
                Console.WriteLine("Type a number to view information on that script. After selecting a script, type a command to use it.\nCommands:\nlist <instructionStart> <instructionEnd>\nset <instructionID> <functionID> <parameter>");
                break;
            case "list":
                instructionStart = 0;
                instructionEnd = 0;
                if (fields.Length == 3 && int.TryParse(fields[1], out instructionStart) && int.TryParse(fields[2], out instructionEnd))
                {
                    for(int i = instructionStart; i <= instructionEnd; i++)
                    {
                        ScriptInstruction currentInstruction = selectedScript.GetInstruction(i);
                        Console.WriteLine("Instruction ID: 0x{0:X}\tFunction ID: 0x{1:X}\tParameter: {2:D}", i, currentInstruction.FunctionID, currentInstruction.Parameter);
                    }
                }
                else
                {
                    Console.WriteLine("Format: list <instructionStart> <instructionEnd>");
                }
                break;
            case "listd":
                instructionStart = 0;
                instructionEnd = 0;
                if (fields.Length == 3 && int.TryParse(fields[1], out instructionStart) && int.TryParse(fields[2], out instructionEnd))
                {
                    for (int i = instructionStart; i <= instructionEnd; i++)
                    {
                        ScriptInstruction currentInstruction = selectedScript.GetInstruction(i);
                        Console.WriteLine("Instruction ID: {0:D}\tFunction ID: {1:D}\tParameter: {2:D}", i, currentInstruction.FunctionID, currentInstruction.Parameter);
                    }
                }
                else
                {
                    Console.WriteLine("Format: listd <instructionStart> <instructionEnd>");
                }
                break;
            case "listx":
                instructionStart = 0;
                instructionEnd = 0;
                if (fields.Length == 3 && int.TryParse(fields[1], out instructionStart) && int.TryParse(fields[2], out instructionEnd))
                {
                    for (int i = instructionStart; i <= instructionEnd; i++)
                    {
                        ScriptInstruction currentInstruction = selectedScript.GetInstruction(i);
                        Console.WriteLine("Instruction ID: 0x{0:X}\tFunction ID: 0x{1:X}\tParameter: 0x{2:X}", i, currentInstruction.FunctionID, currentInstruction.Parameter);
                    }
                }
                else
                {
                    Console.WriteLine("Format: listx <instructionStart> <instructionEnd>");
                }
                break;
            case "highlight":
                if(selectedScript != null && selectedScript.Entity.EntityPtr != 0)
                {
                    Entity target = selectedScript.Entity;
                    if(target.Red == 0)
                    {
                        target.Red = 1;
                        target.Green = 1;
                        target.Blue = 1;
                    }
                    else
                    {
                        target.Red = 0;
                        target.Green = 0;
                        target.Blue = 0;
                    }
                }
                break;
            case "set":
                int instructionSelector = 0;
                int functionSelector = 0;
                int paramSelector = 0;
                if(fields.Length == 4 && int.TryParse(fields[1], out instructionSelector) && int.TryParse(fields[2], out functionSelector) && int.TryParse(fields[3], out paramSelector))
                {
                    ScriptInstruction targetInstruction = selectedScript.GetInstruction(instructionSelector);
                    targetInstruction.FunctionID = (byte)functionSelector;
                    targetInstruction.Parameter = paramSelector;
                }
                else
                {
                    Console.WriteLine("Format: set <instructionID> <functionID> <parameter>");
                }
                break;
            default:
                if(userInput != "exit")
                {
                    Console.WriteLine("Unrecognized command.");
                }
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
        Console.WriteLine("Script ID: {0:D}\nEntity: {1}\nScript ID Code: 0x{2:X}\nCurrent Instruction: 0x{3:X}\t| {3:D}\nCurrent Function: 0x{4:X}\t\t| {4:D}\nCurrent Parameter: 0x{5:X}\t\t| {5:D}\n", selectionIndex, entityName, selectedScript.ScriptIDCode, selectedScript.CurrentInstructionID, currentInstruction.FunctionID, currentInstruction.Parameter);
    }
}
while (userInput != "exit");

memoryInterface.Close();